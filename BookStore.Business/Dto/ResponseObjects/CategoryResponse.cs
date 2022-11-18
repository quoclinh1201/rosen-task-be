using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.ResponseObjects
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
