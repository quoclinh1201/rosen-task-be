using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.RequestObjects
{
    public class CreateOrderRequest
    {
        public string PaymentMethod { get; set; }
        public int DeliveryInformation { get; set; }
    }
}
