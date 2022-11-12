using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.ResponseObjects
{
    public class ProductDetailResponse
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public List<ProductImageResponse> ProductImages { get; set; } = new List<ProductImageResponse>();
        public string Category { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        public string SupplierName { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }
        public string NameOfPublisher { get; set; }
        public int YearOfPublication { get; set; }
        public int Weight { get; set; }
        public string Size { get; set; }
        public int NumberOfPages { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
