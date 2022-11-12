using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.ResponseObjects
{
    public class GetListOrderResponse
    {
        public int OrderId { get; set; }
        public string TotalPrice { get; set; }
        public string CreateDate { get; set; }
        public string Status { get; set; }
    }
}
