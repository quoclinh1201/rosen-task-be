using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using BookStore.Business.Helpers.Constants;
using BookStore.Business.ISerices;
using BookStore.Data.Entities;
using BookStore.Data.IRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IGenericRepository<Account> accountRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetOwnProfileResponse>> ChangeAvatar(string token, IFormFile file)
        {
            var response = new Result<GetOwnProfileResponse>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var user = await _userRepository.FindAsync(u => u.Id == uid);

                var container = new BlobContainerClient(AzureStorageBlobConstants.BlobStorageConnectionString, AzureStorageBlobConstants.BlobStorageContainerName);
                var blob = container.GetBlobClient("avt_" + uid + file.FileName.Substring(file.FileName.IndexOf('.')));

                if (user.AvatarUrl != null)
                {
                    await blob.DeleteIfExistsAsync();

                }
                var stream = file.OpenReadStream();
                var responsee = await blob.UploadAsync(stream);

                user.AvatarUrl = blob.Uri.AbsoluteUri;
                await _userRepository.UpdateAsync(user);
                response.Content = _mapper.Map<GetOwnProfileResponse>(user);
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<Result<GetOwnProfileResponse>> GetOwnProfile(string token)
        {
            var response = new Result<GetOwnProfileResponse>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var user = await _userRepository.FindAsync(u => u.Id == uid);
                var account = await _accountRepository.FindAsync(a => a.Id == uid);

                response.Content = _mapper.Map<GetOwnProfileResponse>(user);
                response.Content.IsFacebookAccount = account.FacebookId != null ? true : false;
                if(response.Content.AvatarUrl == null)
                {
                    response.Content.AvatarUrl = "https://tleliteracy.com/wp-content/uploads/2017/02/default-avatar.png";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return response;
            }
        }

        public async Task<Result<GetOwnProfileResponse>> UpdateOwnProfile(string token, UpdateProfileRequest request)
        {
            var response = new Result<GetOwnProfileResponse>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var user = await _userRepository.FindAsync(u => u.Id == uid);
                user.FullName = request.FullName;
                user.PhoneNumber = request.PhoneNumber;
                user.Email = request.Email;
                user.Gender = request.Gender;
                await _userRepository.UpdateAsync(user);
                response.Content = _mapper.Map<GetOwnProfileResponse>(user);
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
