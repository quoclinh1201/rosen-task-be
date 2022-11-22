using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.ResponseObjects
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public DeliveryInformaionResponse DeliveryInformaion { get; set; }
        public string CreateDate { get; set; }
        public string TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public List<OrderDetailResponse> OrderDetail { get; set; } = new List<OrderDetailResponse>();
    }
}
