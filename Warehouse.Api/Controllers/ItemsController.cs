﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Api.Models.DTOs;
using Warehouse.Api.Services;

namespace Warehouse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsService _itemsService;

        public ItemsController(ItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpPost("add")]
        public async Task<ItemDTO> Add(ItemDTO itemDTO)
                => await _itemsService.Add(itemDTO);

        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> Get()
                => await _itemsService.Get();
    }
}