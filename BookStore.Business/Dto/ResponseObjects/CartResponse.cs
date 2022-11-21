using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.ResponseObjects
{
    public class CartResponse
    {
        public List<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();
        public string TotalPrice { get; set; }
    }
}
