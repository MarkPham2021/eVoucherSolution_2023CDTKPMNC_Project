using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using Microsoft.AspNetCore.Identity;

namespace eVoucher_BUS.Services
{
    public interface IStaffService
    {
        List<Staff> GetAllStaffs();

        Task<Staff?> GetStaffById(int id);

        Task<Staff?> RegisterStaff(StaffRegisterRequest request);

        Task<Staff?> UpdateStaff(StaffUpdateRequest request);

        Task<Staff> DeleteStaff(int id);

        Task<Staff> DeleteStaff(Staff staff);
    }

    public class StaffService : IStaffService
    {
        private StaffRepository _staffRepository;
        private readonly UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public StaffService(StaffRepository staffRepository, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _staffRepository = staffRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<Staff> DeleteStaff(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Staff> DeleteStaff(Staff staff)
        {
            throw new NotImplementedException();
        }

        public List<Staff> GetAllStaffs()
        {
            var Staffs = _staffRepository.GetAll().ToList();
            return Staffs;
        }

        public Task<Staff?> GetStaffById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Staff?> RegisterStaff(StaffRegisterRequest request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
            var result = await _userManager.CreateAsync(user);            
            var staff = new Staff()
            {
                Name = request.Name,
                Gender = request.Gender,
                Department = request.Department,
                CreatedBy = request.CreatedBy,
                CreatedTime = request.CreatedTime,
                IsDeleted = false,
                Status = ActiveStatus.Active,
                AppUser = user
            };            
            var registerResult = await _staffRepository.Add(staff);
            
            return registerResult;
        }

        public Task<Staff?> UpdateStaff(StaffUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}