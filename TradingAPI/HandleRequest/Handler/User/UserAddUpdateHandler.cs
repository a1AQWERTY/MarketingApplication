using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.User;

namespace TradingAPI.HandleRequest.Handler.User
{
    public class UserAddUpdateHandler : IRequestHandler<RequestAddUpdateUser, Guid>
    {
        readonly IUserMasterRepository _userMasterRepository;
        readonly IMapper _mapper;
        public UserAddUpdateHandler(IUserMasterRepository userMasterRepository, IMapper mapper)
        {
            _userMasterRepository = userMasterRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(RequestAddUpdateUser request, CancellationToken cancellationToken)
        {
            UserMaster userMasterResult = null;

            if (request.UserId == Guid.Empty)
            {
                userMasterResult = await AddUser(request);
            }
            else
            {
                userMasterResult = await UpdateUser(request);
            }
            return (userMasterResult == null) ? userMasterResult.UserId : Guid.Empty;
        }

        public async Task<UserMaster> AddUser(RequestAddUpdateUser requestAddUpdateUser)
        {
            UserMaster userMasterAdd = _mapper.Map<UserMaster>(requestAddUpdateUser);

            await _userMasterRepository.AddAsync(userMasterAdd);
            await _userMasterRepository.SaveChangesAsync();

            return userMasterAdd;
        }

        public async Task<UserMaster> UpdateUser(RequestAddUpdateUser requestAddUpdateUser)
        {
            UserMaster userMasterupdate = _mapper.Map<UserMaster>(requestAddUpdateUser);

            await _userMasterRepository.UpdateAsync(userMasterupdate);
            await _userMasterRepository.SaveChangesAsync();

            return userMasterupdate;
        }
    }
}
