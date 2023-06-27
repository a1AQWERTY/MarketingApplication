using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Interface.Interface;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Request.User;
using TradingAPI.HandleRequest.Response.Item;
using TradingAPI.HandleRequest.Response.Users;
using System.Linq;
using Trading.Data.Entities;

namespace TradingAPI.HandleRequest.Handler.User
{
    public class GetUserListHandler : IRequestHandler<RequestUsers, ResponseListClass<List<ResponseGetUserList>>>
    {
        private readonly IUserMasterRepository _userMasterRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userMasterRepository"></param>
        /// <param name="mapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public GetUserListHandler(IUserMasterRepository userMasterRepository, IMapper mapper)
        {
            _userMasterRepository = userMasterRepository ?? throw new ArgumentNullException(nameof(userMasterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseListClass<List<ResponseGetUserList>>> Handle(RequestUsers request, CancellationToken cancellationToken)
        {
            await Task.Run(() => { });

            var userData = _userMasterRepository.GetUsers(request.requestFilter.PageNo, request.requestFilter.PageSize, out int count).Result;

            List<ResponseGetUserList> responseGetUserLists = new List<ResponseGetUserList>();

            if (userData?.Any() == true)
            {
                responseGetUserLists = _mapper.Map<List<UserMaster>, List<ResponseGetUserList>>(userData.ToList());
            }

            return new ResponseListClass<List<ResponseGetUserList>>()
            {
                count = count,
                Data = responseGetUserLists
            };

        }
    }
}
