﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views;
using PracticeControl.WebAPI.Views.blanks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;

namespace PracticeControl.WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IGetService _getService;
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config, IAuthRepository authRepository, IGetService getService)
        {

            _config = config;
            _authRepository = authRepository;
            _getService = getService;
        }
        public AuthResponse Authenticate(string login, string password)
        {

            Employee? employee = _authRepository.GetEmployee(login); 

            if (employee is not null)
            {
                var passwordHash = PasswordHelper.GetHash(employee?.Passwordsalt, password);

                if (passwordHash == employee?.Passwordhash)
                {
                    string token = CreateToken(employee);
                    EmployeeView? userView = _getService.GetEmployee(Convert.ToInt32(employee.Id));
                    return new AuthResponse(token, userView);
                }
            }
          
            return null;
        }

        public string CreateToken(Employee employee)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.Login)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config.GetSection("SecretKey").Value!));

            var credit = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims, expires: DateTime.Now.AddDays(1),
                    signingCredentials: credit);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return $"Bearer {jwt}";
        }

    }
}
