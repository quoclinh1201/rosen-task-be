using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
using BookStore.Business.ISerices;
using BookStore.Data.Entities;
using BookStore.Data.IRepository;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Business.Helpers.Constants;

namespace BookStore.Business.Service
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public AccountService(
            IGenericRepository<Account> accountRepository,
            IGenericRepository<User> userRepository,
            IGenericRepository<Cart> cartRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Result<LoginResponse>> CreateAccount(CreateAccountRequest request)
        {
            var result = new Result<LoginResponse>();
            try
            {
                if(await CheckExistedUsername(request.Username) == false)
                {
                    result.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Tài khoản đã tồn tại, vui lòng chọn lại tài khoản khác.");
                    return result;
                }
                if(await CheckExistedPhoneNumber(request.PhoneNumber) == false)
                {
                    result.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Số điện thoại đã có người đăng ký.");
                    return result;
                }
                if (await CheckExistedEmail(request.Email) == false)
                {
                    result.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Email đã có người đăng ký.");
                    return result;
                }
                var account = _mapper.Map<Account>(request);
                await _accountRepository.InsertAsync(account);
                await _accountRepository.SaveAsync();

                var user = _mapper.Map<User>(request);
                user.Id = account.Id;
                await _userRepository.InsertAsync(user);
                await _userRepository.SaveAsync();

                var cart = new Cart { CartId = user.Id };
                await _cartRepository.InsertAsync(cart);
                await _cartRepository.SaveAsync();

                var token = GenerateJSONWebToken(account.Id.ToString(), account.Role);
                result.Content = new LoginResponse { Token = token };
                return result;
            }
            catch (Exception ex)
            {
                result.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return result;
            }
        }

        private async Task<bool> CheckExistedUsername(string username)
        {
            var account = await _accountRepository.FindAsync(u => username.Equals(u.Username));
            return account == null ? true : false;
        }

        private async Task<bool> CheckExistedPhoneNumber(string phoneNumber)
        {
            var user = await _userRepository.FindAsync(u => phoneNumber.Equals(u.PhoneNumber));
            return user == null ? true : false;
        }

        private async Task<bool> CheckExistedEmail(string email)
        {
            var user = await _userRepository.FindAsync(u => email.Equals(u.Email));
            return user == null ? true : false;
        }

        public async Task<Result<LoginResponse>> Login(LoginRequest request)
        {
            var result = new Result<LoginResponse>();
            try
            {
                var account = await _accountRepository
                    .FindAsync(a => a.Username.Equals(request.Username) 
                                && a.Password.Equals(request.Password));

                if(account != null)
                {
                    if(!account.IsActive)
                    {
                        result.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, "Tài khoản đã bị khóa.");
                        return result;
                    }
                    var token = GenerateJSONWebToken(account.Id.ToString(), account.Role);
                    result.Content = new LoginResponse { Token = token};
                    return result;
                }
                result.Error = ErrorHelpers.PopulateError(404, APITypeConstants.NotFound_404, "Sai thông tin tài khoản hoặc mật khẩu.");
                return result;
            }
            catch (Exception ex)
            {
                result.Error = ErrorHelpers.PopulateError(400, APITypeConstants.BadRequest_400, ex.Message);
                return result;
            }
        }

        private string GenerateJSONWebToken(string id, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Uid", id),
                    new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                            _configuration["Jwt:Issuer"],
                                            claims,
                                            expires: DateTime.Now.AddDays(14),
                                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Result<bool>> ChangePassword(string token, ChangePasswordRequest request)
        {
            var response = new Result<bool>();
            try
            {
                var uid = DecodeJWTToken.GetId(token);
                var account = await _accountRepository.FindAsync(a => a.Id == uid && request.OldPassword.Equals(a.Password));
                account.Password = request.Password;
                await _accountRepository.UpdateAsync(account);
                response.Content = true;
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
