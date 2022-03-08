using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Api.Models.DTOs;
using Warehouse.Database.Models;
using Warehouse.Database.Repositories;

namespace Warehouse.Api.Services
{
    public class ItemsService
    {
        private readonly IMapper _mapper;
        private readonly ItemsRepository _repository;

        public ItemsService(IMapper mapper,
            ItemsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ItemDTO> Add(ItemDTO itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            await _repository.Add(item);
            return _mapper.Map<ItemDTO>(item);
        }

        public async Task<IEnumerable<ItemDTO>> Get()
            => _mapper.Map<IEnumerable<ItemDTO>>(await _repository.GetAll());

        public async Task Update(ItemDTO itemDTO)
            => await _repository.Update(_mapper.Map<Item>(itemDTO));
    }
}
