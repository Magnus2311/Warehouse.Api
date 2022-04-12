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
                => await _partnersService.GetActive();

        [HttpGet("get-all")]
        public async Task<IEnumerable<PartnerDTO>> GetAll()
                => await _partnersService.GetAll();

        [HttpDelete]
        public async Task Delete([FromBody] string partnerId)
                => await _partnersService.Delete(partnerId);

        [HttpPost("partner-recovery")]
        public async Task<PartnerDTO> RecoverItem([FromBody] string partnerId)
                => await _partnersService.Recover(partnerId);
    }
}
