using System;
using System.Collections.Generic;

namespace BookStore.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            CartDetails = new HashSet<CartDetail>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string SupplierName { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Translator { get; set; } = null!;
        public string NameOfPublisher { get; set; } = null!;
        public int YearOfPublication { get; set; }
        public int Weight { get; set; }
        public string Size { get; set; } = null!;
        public int NumberOfPages { get; set; }
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
