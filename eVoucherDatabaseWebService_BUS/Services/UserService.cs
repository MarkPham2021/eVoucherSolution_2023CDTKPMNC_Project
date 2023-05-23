using eVoucher_BUS.Requests.UserRequests;
using eVoucher_BUS.Response;
using eVoucher_DTO.Models;
using eVoucher_Utility.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Services
{
    public interface IUserService
    {
        Task<APIResult<string>> Authenticate(LoginRequest request);
    }
    public class UserService : IUserService
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = configuration;
        }

        public async Task<APIResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || user.UserTypeId != request.UserTypeId) { user = await _userManager.FindByEmailAsync(request.UserName); }
            if (user == null || user.UserTypeId != request.UserTypeId) { return new APIResult<string>(false,"UserName not found", string.Empty); }            
            var result = await _signInManager.PasswordSignInAsync(user, request.Password,request.Rememberme,false);
            if(!result.Succeeded)
            {
                return new APIResult<string>(false, "Incorrect password", string.Empty);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            { 
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, string.Join(";",roles))                
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            
            return new APIResult<string>(true, "Log in successfully", new JwtSecurityTokenHandler().WriteToken(token));
            
        }
    }
}
