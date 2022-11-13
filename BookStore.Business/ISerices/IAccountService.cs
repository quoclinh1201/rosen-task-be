using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.ISerices
{
    public interface IAccountService
    {
        Task<Result<LoginResponse>> Login(LoginRequest request);
        Task<Result<LoginResponse>> CreateAccount(CreateAccountRequest request);
        Task<Result<bool>> ChangePassword(string token, ChangePasswordRequest request);
    }
}
