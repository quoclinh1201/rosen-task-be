using System;
using System.Collections.Generic;

namespace BookStore.Data.Entities
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public int CartId { get; set; }

        public virtual User CartNavigation { get; set; } = null!;
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
