using AutoMapper;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using BookStore.Business.Helpers.Constants;
using BookStore.Business.ISerices;
using BookStore.Data.Entities;
using BookStore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Service
{
    public class DeliveryInformaionService : IDeliveryInformaionService
    {
        private readonly IGenericRepository<DeliveryInformation> _deliveryInformationRepository;
        private readonly IMapper _mapper;

        public DeliveryInformaionService(IGenericRepository<DeliveryInformation> deliveryInformationRepository, IMapper mapper)
        {
            _deliveryInformationRepository = deliveryInformationRepository;
            _mapper = mapper;
        }

        public async Task<Result<bool>> DeleteDeliveryInformation(string token, int id)
        {
            var response = new Result<bool>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var deliveryInfo = await _deliveryInformationRepository.FindAsync(d => d.DeliveryId == id);
                deliveryInfo.IsActive = false;
                await _deliveryInformationRepository.UpdateAsync(deliveryInfo);
                response.Content = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<DeliveryInformaionResponse> GetDeliveryInformaionById(int id)
        {
            try
            {
                var delivery = await _deliveryInformationRepository.FindAsync(d => d.DeliveryId == id);
                return _mapper.Map<DeliveryInformaionResponse>(delivery);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Result<IEnumerable<DeliveryInformaionResponse>>> GetListDeliveryInformation(string token)
        {
            var response = new Result<IEnumerable<DeliveryInformaionResponse>>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var deliveries = await _deliveryInformationRepository.FindByAsync(d => d.UserId == uid && d.IsActive == true);
                response.Content = _mapper.Map<List<DeliveryInformaionResponse>>(deliveries);
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
