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
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public AccountService(
            IGenericRepository<Account> accountRepository,
            IGenericRepository<User> userRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
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
    }
}
