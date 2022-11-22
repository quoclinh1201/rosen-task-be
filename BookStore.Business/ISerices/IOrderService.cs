using BookStore.Business.Dto.Parameters;
using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.ISerices
{
    public interface IOrderService
    {
        Task<Result<OrderResponse>> GetOrderDetailById(string token, int id);
        Task<Result<int>> CreateOrder(string token, CreateOrderRequest request);
        Task<Result<IEnumerable<GetListOrderResponse>>> GetListOrders(string token);
        Task<Result<bool>> CancelOrder(string token, int id);
        Task<Result<bool>> ReOrder(string token, int id);
    }
}
