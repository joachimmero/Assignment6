using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTeht
{

    [Route("api/players/{playerId}/[controller]")]
    [CustomExceptionFilter]
    
    public class ItemsController : ControllerBase
    {

        ItemsProcessor _ItemsProcessor;
        public ItemsController(ItemsProcessor itemsProcessor){
            _ItemsProcessor = itemsProcessor;
        }
        [HttpGet("{id}")]
        public Task<Item> Get(Guid id, Guid playerId){
            return _ItemsProcessor.Get(id, playerId);
        }
        [HttpGet]
        public Task<List<Item>> Get(Guid playerId){
            return _ItemsProcessor.GetAllItems(playerId);
        }

        [HttpPost]
        public Task<Item> Create(Guid playerId, [FromBody] NewItem newItem){
            return _ItemsProcessor.Create(newItem, playerId);
        }

        [HttpPut("{id}")]
        public Task<Item> Modify(Guid id, [FromBody] ModifiedItem modifiedItem, Guid playerId){
            return _ItemsProcessor.Modify(id, modifiedItem, playerId);
        }
        [HttpDelete("{id}")]
        public Task<Item> Delete(Guid playerId, Guid id){
            return _ItemsProcessor.Delete(id, playerId);
        }
    }
}