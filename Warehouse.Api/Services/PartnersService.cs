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
    public class PartnersService
    {
        private readonly PartnersRepository _partnersRepository;
        private readonly IMapper _mapper;

        public PartnersService(IMapper mapper, PartnersRepository partnersRepository)
        {
            _partnersRepository = partnersRepository;
            _mapper = mapper;
        }

        public async Task<PartnerDTO> Add(PartnerDTO itemDto)
        {
            var item = _mapper.Map<Partner>(itemDto);
            await _partnersRepository.Add(item);
            return _mapper.Map<PartnerDTO>(item);
        }

        public async Task<IEnumerable<PartnerDTO>> GetActive()
            => _mapper.Map<IEnumerable<PartnerDTO>>(await _partnersRepository.GetActive());

        public async Task<IEnumerable<PartnerDTO>> GetAll()
            => _mapper.Map<IEnumerable<PartnerDTO>>(await _partnersRepository.GetAll());

        public async Task Update(PartnerDTO itemDTO)
            => await _partnersRepository.Update(_mapper.Map<Partner>(itemDTO));

        public async Task Delete(string itemId)
            => await _partnersRepository.Delete(new ObjectId(itemId));

        public async Task<PartnerDTO> Recover(string partnerId)
            => _mapper.Map<PartnerDTO>(await _partnersRepository.Recover(new ObjectId(partnerId)));
    }
}
