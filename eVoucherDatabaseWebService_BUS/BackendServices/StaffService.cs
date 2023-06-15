using Abp.Threading.Extensions;
using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.StaffRequests;
using Microsoft.AspNetCore.Identity;

namespace eVoucher_BUS.Services
{
    public interface IStaffService
    {
        Task<List<Staff>> GetAllStaffs();

        Task<Staff?> GetStaffById(int id);

        Task<Staff?> RegisterStaff(StaffRegisterRequest request);

        Task<Staff?> UpdateStaff(StaffUpdateRequest request);

        Task<Staff> DeleteStaff(int id);

        Task<Staff> DeleteStaff(Staff staff);
        Task<Staff> Activate(int id);
        Task<Staff> Lock(int id);
    }

    public class StaffService : IStaffService
    {
        private IStaffRepository _staffRepository;
        private readonly UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public StaffService(IStaffRepository staffRepository, UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager)
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

        public async Task<List<Staff>> GetAllStaffs()
        {
            var Staffs = await _staffRepository.GetMulti(s=>s.IsDeleted == false, includes: new string[] {"AppUser"});
            return Staffs;
        }

        public async Task<Staff?> GetStaffById(int id)
        {
            var staff = await _staffRepository.GetSingleByCondition(s=>s.Id == id, includes: new string[] { "AppUser" });
            return staff;
        }

        public async Task<Staff?> RegisterStaff(StaffRegisterRequest request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserTypeId = request.UserTypeId
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
                Status = ActiveStatus.InActive,
                AppUser = user
            };
            var registerResult = await _staffRepository.Add(staff);

            return registerResult;
        }

        public Task<Staff?> UpdateStaff(StaffUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Staff> Activate(int id)
        {
            var staff = await _staffRepository.GetSingleByCondition(s=>s.Id==id);
            staff.Status = ActiveStatus.Active;
            return await _staffRepository.Update(staff);            
        }

        public async Task<Staff> Lock(int id)
        {
            var staff = await _staffRepository.GetSingleById(id);
            staff.Status = ActiveStatus.InActive;
            return await _staffRepository.Update(staff);
        }
    }
}