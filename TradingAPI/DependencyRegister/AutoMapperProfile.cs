using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Data.BusinessEntity;
using Trading.Data.Entities;
using TradingAPI.HandleRequest.Request.Company;
using TradingAPI.HandleRequest.Response;
using TradingAPI.HandleRequest.Response.Item;
using TradingAPI.HandleRequest.Response.Unit;

namespace TradingAPI.DependencyRegister
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ItemMaster, ResponseItem>();

            CreateMap<RequestAddUpdateCompany, CompanyMaster>();

            CreateMap<UnitMaster, ResponseGetUnitMaster>();

            CreateMap<ItemDetailList, ResponseGetItemMasterList>();

            CreateMap<ItemBoMDetail, ResponseGetItemBoMDetail>();

            CreateMap<ItemBoMChildDetail, ResponseItemBoMChildDetail>();

            CreateMap<UserMaster, ResponseUserDetail>();

            CreateMap<ItemMaster, ResponseGetItemDetail>();

            CreateMap<ItemInventoryList, ResponseGetItemInventoryList>();

            CreateMap<ItemInventoryList, ResponseGetItemInventoryDetail>();

            CreateMap<ItemBomList, ResponseGetItemBomList>();

            CreateMap<ItemInventoryList, ResponseGetItemWiseInventoryList>();

        }
    }
}
