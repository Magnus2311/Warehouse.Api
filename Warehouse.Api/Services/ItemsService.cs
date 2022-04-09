using AutoMapper;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
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
            item.Provisions = item.Provisions.Append(new Provision
            {
                Qtty = itemDto.Qtty,
                BasePrice = itemDto.BasePrice
            });

            await _repository.Add(item);
            return _mapper.Map<ItemDTO>(item);
        }

        public async Task<IEnumerable<ItemDTO>> Get()
            => _mapper.Map<IEnumerable<ItemDTO>>(await _repository.GetAll());

        public async Task Update(ItemDTO itemDTO)
            => await _repository.Update(_mapper.Map<Item>(itemDTO));

        public async Task Delete(string itemId)
            => await _repository.Delete(new ObjectId(itemId));

        public async Task<BuyItemDTO> BuyItem(BuyItemDTO buyItemDTO)
        {
            var item = await _repository.Get(new ObjectId(buyItemDTO.ItemId));
            item.Provisions = item.Provisions.Append(new Provision
            {
                Qtty = buyItemDTO.Qtty,
                BasePrice = buyItemDTO.BasePrice
            });
            await _repository.UpdateWithoutHistory(item);
            buyItemDTO.BasePrice = item.Provisions.Average(p => p.BasePrice);
            return buyItemDTO;
        }
    }
}
