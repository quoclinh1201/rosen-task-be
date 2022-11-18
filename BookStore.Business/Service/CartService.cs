using AutoMapper;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using BookStore.Business.Helpers.Constants;
using BookStore.Business.ISerices;
using BookStore.Data.Entities;
using BookStore.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Service
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<CartDetail> _cartDetailRepository;
        private readonly IGenericRepository<ProductImage> _productImageRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public CartService(IGenericRepository<Cart> cartRepository,
            IGenericRepository<ProductImage> productImageRepository,
            IGenericRepository<CartDetail> cartDetailRepository,
            IGenericRepository<Product> productRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productImageRepository = productImageRepository;
            _cartDetailRepository = cartDetailRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<bool>> AddToCart(string token, int id)
        {
            var response = new Result<bool>();
            try
            {
                if(await CheckExistedProduct(id) == false) 
                {
                    response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Sản phẩm không tồn tại.");
                    return response;
                }

                var isNew = true;
                var uid = DecodeJWTToken.GetId(token);
                var cartItems = await _cartDetailRepository.FindByAsync(c => c.CartId == uid);
                for (int i = 0; i < cartItems.Count; i++)
                {
                    if (cartItems.ElementAt(i).ProductId == id)
                    {
                        cartItems.ElementAt(i).Quantity += 1;
                        await _cartDetailRepository.UpdateAsync(cartItems.ElementAt(i));
                        isNew = false;
                        break;
                    }
                }

                if(isNew)
                {
                    var cartItem = new CartDetail { CartId = uid, ProductId = id, Quantity = 1 };
                    await _cartDetailRepository.InsertAsync(cartItem);
                    await _cartDetailRepository.SaveAsync();
                }

                response.Content = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        private async Task<bool> CheckExistedProduct(int id)
        {
            var product = await _productRepository.FindAsync(p => p.ProductId == id && p.IsActive == true);
            if (product != null) return true;
            return false;
        }

        public async Task<Result<List<CartItemResponse>>> GetCart(string token)
        {
            var response = new Result<List<CartItemResponse>>();
            try
            {
                var listItems = new List<CartItemResponse>();
                var uid = DecodeJWTToken.GetId(token);
                var cartItems = await _cartRepository.GetAllByIQueryable()
                    .Include(c => c.CartDetails)
                    .ThenInclude(cd => cd.Product)
                    .Where(c => c.CartId == uid)
                    .FirstOrDefaultAsync();

                if(cartItems != null)
                {
                    foreach (var item in cartItems.CartDetails)
                    {
                        var price = item.Quantity * item.Product.Price;
                        var image = await _productImageRepository.GetAllByIQueryable().Where(i => i.ProductId == item.ProductId).FirstOrDefaultAsync();
                        var i = new CartItemResponse
                        {
                            ProductId = item.ProductId,
                            ProductName = item.Product.ProductName,
                            Quantity = item.Quantity,
                            Price = FormatMoney.FormatPrice(item.Product.Price),
                            SubTotalPrice = FormatMoney.FormatPrice(price),
                            ProductImageUrl = image.ImageUrl
                        };
                        listItems.Add(i);
                    }
                }
                response.Content = listItems;
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<Result<List<CartItemResponse>>> IncreaseProduct(string token, int id)
        {
            var response = new Result<List<CartItemResponse>>();
            try
            {
                if(await CheckExistedProduct(id) == false)
                {
                    response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Sản phẩm không tồn tại.");
                    return response;
                }

                var uid = DecodeJWTToken.GetId(token);
                var cartItem = await _cartDetailRepository.FindAsync(c => c.CartId == uid && c.ProductId == id);
                cartItem.Quantity++;
                await _cartDetailRepository.UpdateAsync(cartItem);
                return await GetCart(token);
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<Result<List<CartItemResponse>>> DecreaseProduct(string token, int id)
        {
            var response = new Result<List<CartItemResponse>>();
            try
            {
                if (await CheckExistedProduct(id) == false)
                {
                    response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Sản phẩm không tồn tại.");
                    return response;
                }

                var uid = DecodeJWTToken.GetId(token);
                var cartItem = await _cartDetailRepository.FindAsync(c => c.CartId == uid && c.ProductId == id);

                if(cartItem.Quantity != 1)
                {
                    cartItem.Quantity--;
                    await _cartDetailRepository.UpdateAsync(cartItem);
                }

                return await GetCart(token);
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<Result<List<CartItemResponse>>> RemoveProduct(string token, int id)
        {
            var response = new Result<List<CartItemResponse>>();
            try
            {
                if (await CheckExistedProduct(id) == false)
                {
                    response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Sản phẩm không tồn tại.");
                    return response;
                }

                var uid = DecodeJWTToken.GetId(token);
                var cartItem = await _cartDetailRepository.FindAsync(c => c.CartId == uid && c.ProductId == id);

                if (cartItem != null)
                {
                    await _cartDetailRepository.DeleteSpecificFieldByAsync(c => c.CartId == uid && c.ProductId == id );
                    await _cartDetailRepository.SaveAsync();
                }

                return await GetCart(token);
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }
    }
}
