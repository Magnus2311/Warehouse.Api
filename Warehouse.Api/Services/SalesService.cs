using AutoMapper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Api.Models.DTOs;
using Warehouse.Database.Models;
using Warehouse.Database.Repositories;

namespace Warehouse.Api.Services
{
    public class SalesService
    {
        private readonly IMapper _mapper;
        private readonly SalesRepository _repository;

        public SalesService(IMapper mapper,
            SalesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<SaleDTO> Add(SaleDTO saleDTO)
        {
            var sale = _mapper.Map<Sale>(saleDTO);
            await _repository.Add(sale);
            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task<IEnumerable<SaleDTO>> GetActive()
            => _mapper.Map<IEnumerable<SaleDTO>>(await _repository.GetActive());

        public async Task Update(SaleDTO saleDTO)
            => await _repository.Update(_mapper.Map<Sale>(saleDTO));

        public async Task<IEnumerable<SaleDTO>> GetAll()
            => _mapper.Map<IEnumerable<SaleDTO>>(await _repository.GetAll());

        public async Task Delete(string saleId)
            => await _repository.Delete(new ObjectId(saleId));
    }
}
