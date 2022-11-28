using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.Company;

namespace TradingAPI.HandleRequest.Handler.Company
{
    public class UserAddUpdateHandler : IRequestHandler<RequestAddUpdateCompany, Guid>
    {
        readonly ICompanyMasterRepository _companyMasterRepository;
        readonly IMapper _mapper;
        public UserAddUpdateHandler(ICompanyMasterRepository companyMasterRepository, IMapper mapper)
        {
            _mapper = mapper;
            _companyMasterRepository = companyMasterRepository;
        }

        public async Task<Guid> Handle(RequestAddUpdateCompany request, CancellationToken cancellationToken)
        {
            CompanyMaster companyMasterResult = null;
            if (request.CompanyMasterId == Guid.Empty)
            {
                companyMasterResult = await AddCompany(request);
            }
            return (companyMasterResult != null) ? companyMasterResult.CompanyMasterId : Guid.Empty;
        }

        private async Task<CompanyMaster> AddCompany(RequestAddUpdateCompany requestAddUpdateCompany)
        {
            CompanyMaster companyMasterAdd = _mapper.Map<CompanyMaster>(requestAddUpdateCompany);
            companyMasterAdd.CreatedBy = requestAddUpdateCompany.requestUserId;
            companyMasterAdd.CreatedDate = DateTime.Now;
            await _companyMasterRepository.AddAsync(companyMasterAdd);
            await _companyMasterRepository.SaveChangesAsync();

            return companyMasterAdd;
        }
    }
}
