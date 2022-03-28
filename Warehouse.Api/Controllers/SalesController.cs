using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Api.Models.DTOs;
using Warehouse.Api.Services;

namespace Warehouse.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _salesService;

        public SalesController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost]
        public async Task<SaleDTO> Add(SaleDTO saleDTO)
                => await _salesService.Add(saleDTO);

        [HttpPut]
        public async Task Update(SaleDTO saleDTO)
                => await _salesService.Update(saleDTO);

        [HttpGet]
        public async Task<IEnumerable<SaleDTO>> Get()
                => await _salesService.Get();

        [HttpDelete]
        public async Task Delete([FromBody] ObjectId saleId)
                => await _salesService.Delete(saleId);
    }
}
