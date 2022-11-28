using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Common;
using Trading.Data.Entities;

using Trading.Interface.Interface;

using TradingAPI.HandleRequest.Request.User;
using TradingAPI.HandleRequest.Response.Item;
using Google.Authenticator;
using Trading.Exception;

namespace TradingAPI.HandleRequest.Handler.Common
{
    public class VerifyTFAHandler : IRequestHandler<RequestVerifyTFA, ResponseUserDetail>
    {
        readonly IUserMasterRepository _userMasterRepository;
        readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public VerifyTFAHandler(IUserMasterRepository userMasterRepository, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _userMasterRepository = userMasterRepository;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }

        public async Task<ResponseUserDetail> Handle(RequestVerifyTFA request, CancellationToken cancellationToken)
        {
            //throw new CustomException("I couldn't execute your request due to bad search criteria.");

            UserMaster userMasterResponse = _userMasterRepository.GetByWhere(k => k.UserEmail.ToLower() == request.UserEmail.ToLower() && !k.IsDeleted)?.FirstOrDefault();

            if (userMasterResponse != null)
            {
                if(string.IsNullOrWhiteSpace(userMasterResponse.UserUnId))
                {
                    CommonException.GeneralException("Please validate user first");
                }
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

                bool isValid = tfa.ValidateTwoFactorPIN(userMasterResponse.UserUnId, request.TFACode);
                //If TFA is valid then only generate token
                if (isValid)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.JWT_Secret));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new[] {
                        new Claim("UserName", userMasterResponse.UserEmail),
                        new Claim("UserId", userMasterResponse.UserId.ToString())};

                    var tokenBody = new JwtSecurityToken(_appSetting.Issuer,
                        _appSetting.Issuer,
                        claims,
                        expires: DateTime.Now.AddHours(24),
                        signingCredentials: credentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(tokenBody);

                    ResponseUserDetail responseUserDetail = _mapper.Map<UserMaster, ResponseUserDetail>(userMasterResponse);
                    responseUserDetail.Token = token;

                    return responseUserDetail;
                }

            }

            return null;
        }
    }
}
