using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Api.Helpers.Attributes;
using Warehouse.Api.Models.DTOs;
using Warehouse.Api.Services;

namespace Warehouse.Api.Controllers
{
    [SSO]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsService _itemsService;

        public ItemsController(ItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpPost]
        public async Task<ItemDTO> Add(ItemDTO itemDTO)
                => await _itemsService.Add(itemDTO);

        [HttpPut]
        public async Task Update(ItemDTO itemDTO)
                => await _itemsService.Update(itemDTO);

        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> Get()
                => await _itemsService.GetActive();

        [HttpGet("get-all")]
        public async Task<IEnumerable<ItemDTO>> GetAll()
                => await _itemsService.GetAll();

        [HttpDelete]
        public async Task Delete([FromBody] string itemId)
                => await _itemsService.Delete(itemId);

        [HttpPost("buy-item")]
        public async Task<BuyItemDTO> BuyItem(BuyItemDTO buyItemDTO)
                => await _itemsService.BuyItem(buyItemDTO);

        [HttpPost("item-recovery")]
        public async Task<ItemDTO> RecoverItem([FromBody] string itemId)
                => await _itemsService.Recover(itemId);
    }
}
