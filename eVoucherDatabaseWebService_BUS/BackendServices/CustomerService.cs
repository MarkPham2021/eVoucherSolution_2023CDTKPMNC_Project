using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.CustomerRequests;
using eVoucher_ViewModel.Requests.VoucherRequests;
using eVoucher_ViewModel.Response;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace eVoucher_BUS.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>?> GetAllCustomersFullInfo();

        Task<Customer?> GetCustomerById(int id);

        Task<Customer?> RegisterCustomer(CustomerRegisterRequest request);

        Task<Customer?> UpdateCustomer(CustomerUpdateRequest request);

        Task<Customer> DeleteCustomer(int id);

        Task<Customer> DeleteCustomer(Customer customer);
        Task<APIClaimVoucherResult> ClaimVoucher(CustomerPlayGameForVoucherRequest request);
        Task<Customer> GetCustomerFullInfoById(int id);
        Task<Customer> GetCustomerFullInfoByUserInfo(string userinfo);
        Task<List<VoucherVM>?> GetAllVouchersOfCustomerByUserInfo(string userinfo);
        Task<List<VoucherVM>?> GetAllVouchersOfCustomerByCustomerId(int id);
        Task<VoucherVM?> GetVoucherVMById(int id);
    }

    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private ICampaignRepository _campaignRepository;
        private IVoucherTypeRepository _voucherTypeRepository;
        private IVoucherTypeImageRepository _voucherTypeImageRepository;
        private IVoucherRepository _voucherRepository;
        private ICampaignGameRepository _campaignGameRepository;
        private IGamePlayResultRepository _gamePlayResultRepository;
        private IGameRepository _gameRepository;
        private readonly UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public CustomerService(ICustomerRepository customerRepository, IVoucherTypeRepository voucherTypeRepository,
            IVoucherRepository voucherRepository, IGamePlayResultRepository gamePlayResultRepository, 
            IGameRepository gameRepository, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            ICampaignGameRepository campaignGameRepository, ICampaignRepository campaignRepository, 
            IVoucherTypeImageRepository voucherTypeImageRepository)
        {
            _customerRepository = customerRepository;            
            _voucherTypeRepository = voucherTypeRepository;
            _voucherRepository = voucherRepository;
            _gamePlayResultRepository = gamePlayResultRepository;
            _gameRepository = gameRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _campaignGameRepository = campaignGameRepository;
            _campaignRepository = campaignRepository;
            _voucherTypeImageRepository = voucherTypeImageRepository;
        }

        public Task<Customer> DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> DeleteCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>?> GetAllCustomersFullInfo()
        {
            return await _customerRepository.GetAllCustomersFullInfo();
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await _customerRepository.GetSingleByCondition(c=>c.Id == id, includes: new string[] {"AppUsers"});
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
                AppUsers = user
            };
            var registerResult = await _customerRepository.Add(customer);

            return registerResult;
        }

        public async Task<Customer?> UpdateCustomer(CustomerUpdateRequest request)
        {
            var user = request.AppUser;            
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);            
            var result = await _userManager.UpdateAsync(user);
            var customer = request.Customer;
            customer.AppUsers = user;
            var registerResult = await _customerRepository.Update(customer);
            return registerResult;
        }

        public async Task<APIClaimVoucherResult> ClaimVoucher(CustomerPlayGameForVoucherRequest request)
        {
            var campaigngame = await _campaignGameRepository.GetSingleByCondition(cpg => cpg.Id == request.CampaignGameId);
            if (campaigngame == null)
            {
                return new APIClaimVoucherResult()
                {
                    IsSuccess = false,
                    IsGotVoucher = false,
                    Message = "CampaignGameId wrong",
                    _Voucher = null
                };
            }
            var infos = request.AppUserInfo.Split('|');
            var appuserid = int.Parse(infos[0]);            
            var user = await _userManager.FindByIdAsync(infos[0]);
            if (user == null)
            {
                return new APIClaimVoucherResult()
                {
                    IsSuccess = false,
                    IsGotVoucher = false,
                    Message = "UserInfo wrong",
                    _Voucher = null
                };
            }
            var campaign =await _campaignRepository.GetSingleByCondition(c =>c.CampaignGames.Contains(campaigngame));
            var vouchertypes = await _voucherTypeRepository.GetMulti(vt => vt.Campaign.Id == campaign.Id);
            int numberofvouchertypes = vouchertypes.Count();
            var customer = await _customerRepository.GetSingleByCondition(c => c.AppUsers.Id == appuserid);
            VoucherType vouchertypeget = new VoucherType();
            APIClaimVoucherResult apiclaimvoucherresult = new APIClaimVoucherResult();
            if (numberofvouchertypes <1)
            {
                return new APIClaimVoucherResult()
                {
                    IsSuccess = false,
                    IsGotVoucher = false,
                    Message = "campaign has no vouchertype",
                    _Voucher = null
                };
            }
            if (numberofvouchertypes > 0)
            {
                bool ismatch = false;                
                for (int i = 0; i < numberofvouchertypes; i++)
                {
                    var luckynumbers = JsonConvert.DeserializeObject<List<int>>(vouchertypes[i].LuckyNumbers);
                    if (luckynumbers.Contains(request.GottenNumber)) 
                    {
                        vouchertypeget = vouchertypes[i];
                        ismatch = true;
                        break;
                    }
                }                
                if (!ismatch) 
                {
                    vouchertypeget = vouchertypes[numberofvouchertypes - 1];
                    var vouchertypegetimage = await _voucherTypeImageRepository.GetSingleByCondition(img => img.VoucherType.Id == vouchertypeget.Id);
                    apiclaimvoucherresult = new APIClaimVoucherResult()
                    {
                        IsSuccess = true,
                        IsGotVoucher = true,
                        Message = "you got the allgotten voucher",
                        _Voucher = new eVoucher_ViewModel.Requests.VoucherRequests.VoucherTypeVM()
                        {
                            Id = vouchertypeget.Id,
                            Name = vouchertypeget.Name,
                            CampaignId = campaign.Id,
                            CampaignName = campaign.Name,
                            DiscountRate = vouchertypeget.DiscountRate,
                            Promotion = vouchertypeget.Promotion,
                            ImagePath = vouchertypegetimage.ImagePath,
                            ExpiringDate = vouchertypeget.ExpiringDate
                        }
                    };

                }
                else
                {
                    var vouchertypegetimage = await _voucherTypeImageRepository.GetSingleByCondition(img => img.VoucherType.Id == vouchertypeget.Id);
                    apiclaimvoucherresult = new APIClaimVoucherResult()
                    {
                        IsSuccess = true,
                        IsGotVoucher = true,
                        Message = "you win a voucher",
                        _Voucher = new eVoucher_ViewModel.Requests.VoucherRequests.VoucherTypeVM()
                        {
                            Id = vouchertypeget.Id,
                            Name = vouchertypeget.Name,
                            CampaignId = campaign.Id,
                            CampaignName = campaign.Name,
                            DiscountRate = vouchertypeget.DiscountRate,
                            Promotion = vouchertypeget.Promotion,
                            ImagePath = vouchertypegetimage.ImagePath,
                            ExpiringDate = vouchertypeget.ExpiringDate
                        }
                    };
                }
                
            }
            customer.GamePlayResults = new List<GamePlayResult>();
            user.GamePlayResults =new List<GamePlayResult>();
            var gameplayresult = new GamePlayResult()
            { 
                CampaignGame = campaigngame,
                GotNumberResult = request.GottenNumber,
                IsGotVoucher = true,
                VoucherType = vouchertypeget,
                Voucher = new Voucher()
                {                    
                    DateGet = DateTime.Now,
                    VoucherStatus = VoucherStatus.UnUsed
                }
            };
            customer.GamePlayResults.Add(gameplayresult);
            user.GamePlayResults.Add(gameplayresult);
            var updatecustomerresult = await _customerRepository.Update(customer);
            var updateappuserresult = await _userManager.UpdateAsync(user);
            var game = await _gameRepository.GetSingleByCondition(g => g.CampaignGames.Contains(campaigngame));            
            game.PlayedCount += 1;
            vouchertypeget.RemainAmount -= 1;
            var update_remain_amout_of_voucher = await _voucherTypeRepository.Update(vouchertypeget);
            var updategameplaycount = await _gameRepository.Update(game);
            return apiclaimvoucherresult;
        }

        public async Task<Customer> GetCustomerFullInfoById(int id)
        {
            return await _customerRepository.GetCustomerFullInfoById(id);
        }

        public async Task<Customer> GetCustomerFullInfoByUserInfo(string userinfo)
        {
            return await _customerRepository.GetCustomerFullInfoByUserInfo(userinfo);
        }

        public async Task<List<VoucherVM>?> GetAllVouchersOfCustomerByUserInfo(string userinfo)
        {
            int userid = int.Parse(userinfo.Split('|')[0]);
            return await _voucherRepository.GetAllVoucherVMsOfCustomerByAppUserId(userid);
        }

        public async Task<List<VoucherVM>?> GetAllVouchersOfCustomerByCustomerId(int id)
        {
            return await _voucherRepository.GetAllVoucherVMsOfCustomerByCustomerId(id);
        }

        public async Task<VoucherVM?> GetVoucherVMById(int id)
        {
            return await _voucherRepository.GetVoucherVMById(id);
        }
    }
}