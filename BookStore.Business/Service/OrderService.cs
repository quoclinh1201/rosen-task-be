using AutoMapper;
using BookStore.Business.Dto.Parameters;
using BookStore.Business.Dto.RequestObjects;
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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Service
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<CartDetail> _cartRepository;
        private readonly IDeliveryInformaionService _deliveryInformaionService;
        private readonly IGenericRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> orderRepository,
            IMapper mapper,
            IDeliveryInformaionService deliveryInformaionService,
            IGenericRepository<ProductImage> productImageRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<CartDetail> cartRepository,
            IGenericRepository<OrderDetail> orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _deliveryInformaionService = deliveryInformaionService;
            _productImageRepository = productImageRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<Result<bool>> CancelOrder(string token, int id)
        {
            var response = new Result<bool>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var order = await _orderRepository.FindAsync(o => o.OrderId == id && o.UserId == uid);
                order.Status = 0;
                await _orderRepository.UpdateAsync(order);
                response.Content = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<Result<int>> CreateOrder(string token, CreateOrderRequest request)
        {
            var response = new Result<int>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var cartItems = await _cartRepository.GetAllByIQueryable().Include(c => c.Product).Where(c => c.CartId == uid).ToListAsync();
                decimal totalPrice = 0;
                foreach (var item in cartItems)
                {
                    if(item.Quantity > item.Product.Quantity)
                    {
                        response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Số lượng trong giỏ hàng đã vượt quá số lượng hàng trong kho.");
                        return response;
                    }
                    totalPrice += item.Quantity * item.Product.Price;
                }
                var order = _mapper.Map<Order>(request);
                order.UserId = uid;
                order.TotalPrice = totalPrice;
                await _orderRepository.InsertAsync(order);
                await _orderRepository.SaveAsync();

                foreach (var item in cartItems)
                {
                    var product = await _productRepository.FindAsync(p => p.ProductId == item.ProductId);
                    product.Quantity = product.Quantity - item.Quantity;
                    await _productRepository.UpdateAsync(product);

                    var orderDetail = new OrderDetail 
                    { 
                        OrderId = order.OrderId,
                        ProductId = item.ProductId ,
                        Quantity = item.Quantity,
                        Price = item.Quantity * product.Price
                    };
                    await _orderDetailRepository.InsertAsync(orderDetail);
                    await _orderDetailRepository.SaveAsync();
                }

                await _cartRepository.DeleteSpecificFieldByAsync(c => c.CartId == uid);
                await _cartRepository.SaveAsync();
                response.Content = order.OrderId;
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<Result<IEnumerable<GetListOrderResponse>>> GetListOrders(string token)
        {
            var response = new Result<IEnumerable<GetListOrderResponse>>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var orders = await _orderRepository.FindByAsync(o => o.UserId == uid);
                response.Content = _mapper.Map<List<GetListOrderResponse>>(orders.OrderByDescending(o => o.CreateDate));
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Result<OrderResponse>> GetOrderDetailById(string token, int id)
        {
            var response = new Result<OrderResponse>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var order = await _orderRepository.FindAsync(o => o.OrderId == id && o.UserId == uid);
                var result = _mapper.Map<OrderResponse>(order);
                result.DeliveryInformaion = await _deliveryInformaionService.GetDeliveryInformaionById(order.DeliveryId);
                var listItems = new List<OrderDetailResponse>();
                var orderdetails = await _orderDetailRepository.GetAllByIQueryable()
                    .Include(o => o.Product)
                    .Where(o => o.OrderId == result.OrderId)
                    .ToListAsync();
                foreach (var item in orderdetails)
                {
                    var price = item.Quantity * item.Product.Price;
                    var image = await _productImageRepository.GetAllByIQueryable().Where(i => i.ProductId == item.ProductId).FirstOrDefaultAsync();
                    var i = new OrderDetailResponse
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product.ProductName,
                        Quantity = item.Quantity,
                        Price = FormatMoney.FormatPrice(price),
                        ProductImageUrl = image.ImageUrl
                    };
                    listItems.Add(i);
                }
                result.OrderDetail = listItems;
                response.Content = result;
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<Result<bool>> ReOrder(string token, int id)
        {
            var response = new Result<bool>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var orderDetails = await _orderDetailRepository.FindByAsync(o => o.OrderId == id);
                foreach (var item in orderDetails)
                {
                    var ci = await _cartRepository.FindAsync(c => c.CartId == uid && c.ProductId == item.ProductId);
                    if(ci != null)
                    {
                        ci.Quantity = ci.Quantity + item.Quantity;
                        await _cartRepository.UpdateAsync(ci);
                    }
                    else
                    {
                        var cartItem = new CartDetail { CartId = uid, ProductId = item.ProductId, Quantity = item.Quantity };
                        await _cartRepository.InsertAsync(cartItem);
                        await _cartRepository.SaveAsync();
                    }
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
    }
}
