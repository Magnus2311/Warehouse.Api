using AutoMapper;
using Warehouse.Api.Models.DTOs;
using Warehouse.Database.Models;

namespace Warehouse.Api.Services.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ItemDTO, Item>();
            CreateMap<Item, ItemDTO>();

            CreateMap<PartnerDTO, Partner>();
            CreateMap<Partner, PartnerDTO>();

            CreateMap<SaleDTO, Sale>();
            CreateMap<Sale, SaleDTO>();

            CreateMap<SaleItemDTO, SaleItem>();
            CreateMap<SaleItem, SaleItemDTO>();
        }
    }
}
