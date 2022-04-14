using AutoMapper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Api.Models.DTOs;
using Warehouse.Database.Models;
using Warehouse.Database.Repositories;

namespace Warehouse.Api.Services
{
    public class SalesService
    {
        private readonly IMapper _mapper;
        private readonly SalesRepository _salesRepository;
        private readonly ItemsRepository _itemsRepository;

        public SalesService(IMapper mapper,
            SalesRepository repository,
            ItemsRepository itemsRepository)
        {
            _mapper = mapper;
            _salesRepository = repository;
            _itemsRepository = itemsRepository;
        }

        public async Task<SaleDTO> Add(SaleDTO saleDTO)
        {
            var sale = _mapper.Map<Sale>(saleDTO);
            var items = _mapper.Map<IEnumerable<Item>>((await _itemsRepository.GetActive())
                .Where(i => saleDTO.SaleItems
                .Select(si => si.ItemId)
                .Contains(i.Id.ToString())));

            foreach (var saleItem in saleDTO.SaleItems)
            {
                var currentItem = items.FirstOrDefault(i => i.Id.ToString() == saleItem.ItemId);
                currentItem.Provisions = currentItem.Provisions.Append(new Provision
                {
                    Qtty = saleItem.Qtty * -1,
                    BasePrice = saleItem.Price
                });
                await _itemsRepository.UpdateWithoutHistory(currentItem);
            }

            await _salesRepository.Add(sale);
            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task<IEnumerable<SaleDTO>> GetActive()
            => _mapper.Map<IEnumerable<SaleDTO>>(await _salesRepository.GetActive());

        public async Task Update(SaleDTO saleDTO)
            => await _salesRepository.Update(_mapper.Map<Sale>(saleDTO));

        public async Task<IEnumerable<SaleDTO>> GetAll()
            => _mapper.Map<IEnumerable<SaleDTO>>(await _salesRepository.GetAll());

        public async Task Delete(string saleId)
            => await _salesRepository.Delete(new ObjectId(saleId));

        public async Task<SaleDTO> Recover(string saleId)
            => _mapper.Map<SaleDTO>(await _salesRepository.Recover(new ObjectId(saleId)));
    }
}
