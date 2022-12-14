using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.RequestObjects
{
    public class CreateDeliveryInformationRequest
    {
        [Required]
        [MaxLength(50)]
        public string ReceiverName { get; set; }
        [Required]
        [RegularExpression(@"[0]{1}[0-9]{9}")]
        [MaxLength(10)]
        [MinLength(10)]
        public string ReceiverPhoneNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string DeliveryAddress { get; set; }
    }
}
