using System;
using System.Collections.Generic;

namespace BookStore.Data.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int DeliveryId { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public byte Status { get; set; }

        public virtual DeliveryInformation Delivery { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
