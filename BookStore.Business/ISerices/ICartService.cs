using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.ISerices
{
    public interface ICartService
    {
        Task<Result<List<CartItemResponse>>> GetCart(string token);
        Task<Result<bool>> AddToCart(string token, int id);
        Task<Result<List<CartItemResponse>>> IncreaseProduct(string token, int id);
        Task<Result<List<CartItemResponse>>> DecreaseProduct(string token, int id);
        Task<Result<List<CartItemResponse>>> RemoveProduct(string token, int id);
    }
}
