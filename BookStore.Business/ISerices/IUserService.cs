using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.ISerices
{
    public interface IUserService
    {
        Task<Result<GetOwnProfileResponse>> ChangeAvatar(string v, IFormFile file);
        Task<Result<GetOwnProfileResponse>> GetOwnProfile(string token);
        Task<Result<GetOwnProfileResponse>> UpdateOwnProfile(string token, UpdateProfileRequest request);
    }
}
