using BookStore.Business.Dto.Parameters;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.ISerices
{
    public interface IProductService
    {
        Task<PagedResult<GetListProductResponse>> GetListProduct(GetListProductParameter parameter);
        Task<Result<ProductDetailResponse>> GetDetailProduct(int id);
    }
}
