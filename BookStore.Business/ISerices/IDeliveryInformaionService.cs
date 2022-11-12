using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.ISerices
{
    public interface IDeliveryInformaionService
    {
        Task<DeliveryInformaionResponse> GetDeliveryInformaionById(int id);
        Task<Result<IEnumerable<DeliveryInformaionResponse>>> GetListDeliveryInformation(string token);
        Task<Result<bool>> DeleteDeliveryInformation(string token, int id);
    }
}
