using AutoMapper;
using BookStore.Business.Dto.Parameters;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookStore.Business.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductImage> productImageRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<GetListProductResponse>> GetListProduct(GetListProductParameter parameter)
        {
            try
            {
                var response = new List<GetListProductResponse>();
                var products = await _productRepository.GetAllByIQueryable()
                    .Include(p => p.Category)
                    .Where(p => p.IsActive == true)
                    .ToArrayAsync();

                if(products.Length > 0)
                {
                    var query = products.AsQueryable();
                    FilterProductByName(ref query, parameter.ProductName);
                    FilterProductByCategory(ref query, parameter.CategoryName);
                    OrderByPrice(ref query, parameter.OrderByPrice);

                    response = _mapper.Map<List<GetListProductResponse>>(query.ToList());
                    if(response.Count > 0)
                    {
                        for (int i = 0; i < response.Count; i++)
                        {
                            response[i].ProductImageUrl =
                                await _productImageRepository
                                .GetAllByIQueryable()
                                .Where(image => image.ProductId == response[i].ProductId)
                                .Select(image => image.ImageUrl)
                                .FirstOrDefaultAsync();
                        }
                    }
                }
                return PagedResult<GetListProductResponse>.ToPagedList(response, parameter.PageNumber, parameter.PageSize);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void FilterProductByName(ref IQueryable<Product> query, string name)
        {
            if (!query.Any() || String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
            {
                return;
            }

            query = query.Where(q => q.ProductName.ToLower().Contains(name.ToLower()));
        }

        private static void FilterProductByCategory(ref IQueryable<Product> query, string category)
        {
            if (!query.Any() || String.IsNullOrEmpty(category) || String.IsNullOrWhiteSpace(category))
            {
                return;
            }

            query = query.Where(q => q.Category.CategoryName.ToLower().Equals(category.ToLower()));
        }

        private void OrderByPrice(ref IQueryable<Product> query, bool? orderByPrice)
        {
            if (!query.Any() || orderByPrice is null)
            {
                return;
            }

            if (orderByPrice is true)
            {
                query = query.OrderByDescending(x => x.Price);
            }
            else
            {
                query = query.OrderBy(x => x.Price);
            }
        }

        public async Task<Result<ProductDetailResponse>> GetDetailProduct(int id)
        {
            var response = new Result<ProductDetailResponse>();
            try
            {
                var product = await _productRepository
                    .GetAllByIQueryable()
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Where(p => p.ProductId == id && p.IsActive == true)
                    .FirstOrDefaultAsync();

                if (product != null)
                {
                    response.Content = _mapper.Map<ProductDetailResponse>(product);
                    response.Content.ProductImages = _mapper.Map<List<ProductImageResponse>>(product.ProductImages);
                    return response;
                }
                response.Error = ErrorHelpers.PopulateError(404, APITypeConstants.NotFound_404, "Sản phẩm không tồn tại.");
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
