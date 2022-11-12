using System;
using System.Collections.Generic;

namespace BookStore.Data.Entities
{
    public partial class DeliveryInformation
    {
        public DeliveryInformation()
        {
            Orders = new HashSet<Order>();
        }

        public int DeliveryId { get; set; }
        public int UserId { get; set; }
        public string ReceiverName { get; set; } = null!;
        public string ReceiverPhoneNumber { get; set; } = null!;
        public string DeliveryAddress { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
