using System;
using System.Collections.Generic;

namespace BookStore.Data.Entities
{
    public partial class User
    {
        public User()
        {
            DeliveryInformations = new HashSet<DeliveryInformation>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public bool Gender { get; set; }
        public bool IsActive { get; set; }

        public virtual Account IdNavigation { get; set; } = null!;
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<DeliveryInformation> DeliveryInformations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
