using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using eVoucher_Utility.Exceptions;
using eVoucher_ViewModel.Requests.UserRequests;
using eVoucher_ViewModel.Response;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IStaffRepository _staffRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly ICustomerRepository _customerRepository;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            RoleManager<AppRole> roleManager, IConfiguration configuration,
            IStaffRepository staffRepository, IPartnerRepository partnerRepository,
            ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = configuration;
            _staffRepository = staffRepository;
            _partnerRepository = partnerRepository;
            _customerRepository = customerRepository;
        }

        public async Task<APIResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || user.UserTypeId != request.UserTypeId) { user = await _userManager.FindByEmailAsync(request.UserName); }
            if (user == null || user.UserTypeId != request.UserTypeId) { return new APIResult<string>(false,"UserName not found", string.Empty); }            
            //check if user is inactive
            ActiveStatus useractivestatus = ActiveStatus.Active;
            if(request.UserTypeId == 1)
            {
                var u = await _staffRepository.GetSingleByCondition(s=> s.AppUser.Id == user.Id, includes:new string[]{ "AppUser"});
                useractivestatus = u.Status;
            }
            else if (request.UserTypeId == 2)
            {
                var u = await _partnerRepository.GetSingleByCondition(p => p.AppUser.Id == user.Id, includes: new string[] { "AppUser" });
                useractivestatus = u.Status;
            }
            else if (request.UserTypeId == 3)
            {
                var u = await _customerRepository.GetSingleByCondition(c => c.AppUsers.Id == user.Id, includes: new string[] { "AppUsers" });
                useractivestatus = u.Status;
            }
            if(useractivestatus ==ActiveStatus.InActive)
            {
                return new APIResult<string>(false, "This account is inactive, contact admin to activate", string.Empty);
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password,request.Rememberme,false);
            if(!result.Succeeded)
            {
                return new APIResult<string>(false, "Incorrect password", string.Empty);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()+"|"+user.UserName),
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
