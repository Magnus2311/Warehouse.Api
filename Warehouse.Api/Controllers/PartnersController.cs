using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Api.Models.DTOs;
using Warehouse.Api.Services;

namespace Warehouse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly PartnersService _partnersService;

        public PartnersController(PartnersService partnersService)
        {
            _partnersService = partnersService;
        }

        [HttpPost]
        public async Task<PartnerDTO> Add(PartnerDTO partnerDTO)
                => await _partnersService.Add(partnerDTO);

        [HttpPut]
        public async Task Update(PartnerDTO partnerDTO)
                => await _partnersService.Update(partnerDTO);

        [HttpGet]
        public async Task<IEnumerable<PartnerDTO>> Get()
                => await _partnersService.Get();

        [HttpDelete]
        public async Task Delete([FromBody] ObjectId partnerId)
                => await _partnersService.Delete(partnerId);
    }
}
