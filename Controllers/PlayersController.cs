using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeht
{

    [Route ("api/[controller]")]
    public class PlayersController
    {

        PlayerProcessor _PlayerProcessor;
        public PlayersController (PlayerProcessor playerProcessor)
        {
            _PlayerProcessor = playerProcessor;
        }

        [HttpGet ("{id}")]
        public Task<Player> Get (Guid id)
        {
            return _PlayerProcessor.Get (id);
        }

        [HttpGet]
        public Task<List<Player>> GetAll ()
        {
            return _PlayerProcessor.GetAll ();
        }

        [HttpPost]
        public Task<Player> Create ([FromBody] NewPlayer player)
        {
            return _PlayerProcessor.Create (player);
        }

        [HttpPut ("{id}")]
        public Task<Player> Modify (Guid id, [FromBody] ModifiedPlayer player)
        {
            return _PlayerProcessor.Modify (id, player);
        }
        
        [ServiceFilter(typeof(AuditFilter))]
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete ("{id}")]
        public Task<Player> Delete (Guid id)
        {
            return _PlayerProcessor.Delete (id);
        }

    }
}