using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Helpers.Common
{
    public class Result<T>
    {
        public T Content { get; set; }
        public Error Error { get; set; }
        public bool IsSuccess => Error == null;
    }
}
