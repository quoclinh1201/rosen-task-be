using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.RequestObjects
{
    public class FacebookLoginRequest
    {
        public string FacebookId { get; set; }
        public string Name { get; set; }
    }
}
