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
        }
    }
}
