﻿using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.CustomerRequests;
using Microsoft.AspNetCore.Identity;

namespace eVoucher_BUS.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();

        Task<Customer?> GetCustomerById(int id);

        Task<Customer?> RegisterCustomer(CustomerRegisterRequest request);

        Task<Customer?> UpdateCustomer(CustomerUpdateRequest request);

        Task<Customer> DeleteCustomer(int id);

        Task<Customer> DeleteCustomer(Customer customer);
    }

    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private readonly UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public CustomerService(ICustomerRepository customerRepository, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<Customer> DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> DeleteCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer?> RegisterCustomer(CustomerRegisterRequest request)
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
            var customer = new Customer()
            {
                Name = request.Name,
                Gender = request.Gender,
                DOB = request.DOB,
                Address = request.Address,
                CreatedBy = request.CreatedBy,
                CreatedTime = request.CreatedTime,
                IsDeleted = false,
                Status = ActiveStatus.Active,
                AppUser = user
            };
            var registerResult = await _customerRepository.Add(customer);

            return registerResult;
        }

        public Task<Customer?> UpdateCustomer(CustomerUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}