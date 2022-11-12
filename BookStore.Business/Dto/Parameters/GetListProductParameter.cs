using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.Parameters
{
    public class GetListProductParameter : QueryStringParameters
    {
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public bool? OrderByPrice { get; set; }
    }
}
