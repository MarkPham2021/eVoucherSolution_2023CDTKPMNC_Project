using eVoucher.ClientAPI_Integration;
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