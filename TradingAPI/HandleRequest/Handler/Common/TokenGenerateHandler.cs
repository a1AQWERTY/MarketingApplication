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
using TradingAPI.Extensions;
using TradingAPI.HandleRequest.Request.User;
using TradingAPI.HandleRequest.Response.Item;
using Google.Authenticator;
namespace TradingAPI.HandleRequest.Handler.Common
{
    public class TokenGenerateHandler : IRequestHandler<RequestUserToken, ResponseUserDetail>
    {
        readonly IUserMasterRepository _userMasterRepository;
        readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public TokenGenerateHandler(IUserMasterRepository userMasterRepository, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _userMasterRepository = userMasterRepository;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }

        public async Task<ResponseUserDetail> Handle(RequestUserToken request, CancellationToken cancellationToken)
        {

            UserMaster userMasterResponse = _userMasterRepository.GetByWhere(k => k.UserEmail.ToLower() == request.UserEmail.ToLower() && !k.IsDeleted)?.FirstOrDefault();
            if (userMasterResponse != null)
            {

                string UserUniqueKey = userMasterResponse.UserUnId;
                SetupCode setupInfo = null;
                if (string.IsNullOrWhiteSpace(userMasterResponse.UserUnId))
                {
                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                    //Generate Unique key that we are going to use for validate TFA Code
                    UserUniqueKey = (request.UserEmail + Guid.NewGuid().ToString());

                    setupInfo = tfa.GenerateSetupCode("User", request.UserEmail, UserUniqueKey, false, 300);
                }

                ResponseUserDetail responseUserDetail = _mapper.Map<UserMaster, ResponseUserDetail>(userMasterResponse);

                responseUserDetail.QrCodeSetupImageUrl = setupInfo?.QrCodeSetupImageUrl;
                responseUserDetail.ManualEntryKey = setupInfo?.ManualEntryKey;
                responseUserDetail.IsTFARequired = string.IsNullOrWhiteSpace(userMasterResponse.UserUnId);

                await UpdateUserData(userMasterResponse, UserUniqueKey);

                return responseUserDetail;
            }

            return null;
        }

        public async Task UpdateUserData(UserMaster UserData, string UserUniqueKey)
        {
            UserData.UserUnId = UserUniqueKey;

            await _userMasterRepository.UpdateAsync(UserData);
            await _userMasterRepository.SaveChangesAsync();
        }

      
    }
}
