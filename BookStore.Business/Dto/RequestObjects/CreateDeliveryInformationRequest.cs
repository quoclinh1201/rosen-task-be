using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.RequestObjects
{
    public class CreateDeliveryInformationRequest
    {
        public string ReceiverName { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
