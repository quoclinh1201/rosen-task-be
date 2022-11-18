using AutoMapper;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using BookStore.Business.Helpers.Constants;
using BookStore.Business.ISerices;
using BookStore.Data.Entities;
using BookStore.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CategoryResponse>>> GetCategories()
        {
            var response = new Result<IEnumerable<CategoryResponse>>();
            try
            {
                var categories = await _genericRepository.FindByAsync(c => c.IsActive == true);
                response.Content = _mapper.Map<List<CategoryResponse>>(categories);
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }
    }
}
