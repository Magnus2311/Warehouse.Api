using System.Linq;
using AutoMapper;
using MongoDB.Bson;
using Warehouse.Api.Models.DTOs;
using Warehouse.Database.Models;

namespace Warehouse.Api.Services.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ItemDTO, Item>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(src.Id)));
            CreateMap<Item, ItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Qtty, opt => opt.MapFrom(src => src.Provisions.Sum(p => p.Qtty)))
                .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.Provisions.Select(p => p.BasePrice * p.Qtty).Average() / src.Provisions.Sum(p => p.Qtty)));

            CreateMap<PartnerDTO, Partner>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(src.Id)));
            CreateMap<Partner, PartnerDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<SaleDTO, Sale>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(src.Id)));
            CreateMap<Sale, SaleDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<SaleItemDTO, SaleItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(src.Id)));
            CreateMap<SaleItem, SaleItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
