using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.ISerices
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryResponse>>> GetCategories();
    }
}
