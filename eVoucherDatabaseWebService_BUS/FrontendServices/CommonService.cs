using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.Common;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace eVoucher_BUS.FrontendServices
{
    public interface ICommonService
    {
        public Task<TextValueObject> GetDistanceMatrix(GetGoogleDistanceMatrixRequest request);
        public ClaimsPrincipal ValidateToken(string jwtToken);
        string FormatDatetimeToDatetimeLocalStr(DateTime dateTime);
    }
    public class CommonService: ICommonService
    {
        private GoogleDistanceMatrixAPICLient _googleapiclient;
        private IConfiguration _configuration;
        public CommonService(GoogleDistanceMatrixAPICLient googleapiclient, IConfiguration configuration)
        {
            _googleapiclient = googleapiclient;
            _configuration = configuration;
        }

        public string FormatDatetimeToDatetimeLocalStr(DateTime dateTime)
        {
            string bd, bm, bh, bmi;
            if (dateTime.Day > 9)
            {
                bd = dateTime.Day.ToString();
            }
            else
            {
                bd = "0" + dateTime.Day.ToString();
            }
            if (dateTime.Month > 9)
            {
                bm = dateTime.Month.ToString();
            }
            else
            {
                bm = "0" + dateTime.Month.ToString();
            }
            if (dateTime.Hour > 9)
            {
                bh = dateTime.Hour.ToString();
            }
            else
            {
                bh = "0" + dateTime.Hour.ToString();
            }
            if (dateTime.Minute > 9)
            {
                bmi = dateTime.Minute.ToString();
            }
            else
            {
                bmi = "0" + dateTime.Minute.ToString();
            }
            string datetimestr = dateTime.Year.ToString() + "-" + bm + "-" +
                       bd + " " + bh + ":" + bmi;
            return datetimestr;
        }

        public async Task<TextValueObject> GetDistanceMatrix(GetGoogleDistanceMatrixRequest request)
        {
            return await _googleapiclient.GetDistanceMatrix(request);
        }
        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            var jwtTokentrim = jwtToken.Trim(' ', '\n');
            int n = jwtToken.Length;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtTokentrim,
                validationParameters, out validatedToken);
            return principal;
        }
    }
}