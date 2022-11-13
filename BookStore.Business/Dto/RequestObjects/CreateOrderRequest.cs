using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.RequestObjects
{
    public class CreateOrderRequest
    {
        [Required]
        [MaxLength(10)]
        public string PaymentMethod { get; set; }
        [Required]
        public int DeliveryInformation { get; set; }
    }
}
