using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ApiTeht
{
    public class MongoDbRepository : IRepository
    {
        MongoClient _client;
        IMongoDatabase _db;
        // Dictionary<Player, List<Item>> items = new Dictionary<Player, List<Item>> ();
        private readonly IMongoCollection<LogEntry> logCollection;

        public MongoDbRepository ()
        {
            _client = new MongoClient("mongodb://localhost:27017");          
            _db = _client.GetDatabase("Players");
            logCollection = _db.GetCollection<LogEntry>("log");

        }

        public async Task DeleteStarted()
        {
            await logCollection.InsertOneAsync(new LogEntry("A request to delete player started at " + DateTime.Now.ToString()));
        }

        public async Task DeleteSuccess()
        {
            await logCollection.InsertOneAsync(new LogEntry("A request to delete player ended at " + DateTime.Now.ToString()));
        }
        public async Task<Player> Get (Guid id)
        {
            var filter = Builders<Player>.Filter.Eq("Id", id);
            var result = await _db.GetCollection<Player>("Players").Find(filter).FirstOrDefaultAsync();
            return result;
 
        }

        public async Task<List<Player>> GetAll ()
        {
            var filter = Builders<Player>.Filter.Empty;
            var result = await _db.GetCollection<Player>("Players").Find(filter).ToListAsync();
            return result.ToList();
        }

    public async Task<Player> Create (Player player)
        {
            Player _player = new Player ();
            _player = player;
            await _db.GetCollection<Player>("Players").InsertOneAsync(_player);
            return _player;
        }

        public async Task<Player> Modify (Guid id, ModifiedPlayer player)
        {
 
            var filter = Builders<Player>.Filter.Eq("Id", id);
            Player non_modified_player = await _db.GetCollection<Player>("Players").Find(filter).FirstOrDefaultAsync();
            non_modified_player.Score = player.Score;
            await _db.GetCollection<Player>("Players").DeleteOneAsync(filter);
            await _db.GetCollection<Player>("Players").InsertOneAsync(non_modified_player);
            return non_modified_player;
        }
        public async Task<Player> Delete (Guid id)
        {

            var filter = Builders<Player>.Filter.Eq("Id", id);
            Player _player = new Player();
            _player = await _db.GetCollection<Player>("Players").Find(filter).FirstOrDefaultAsync();
            await _db.GetCollection<Player>("Players").DeleteOneAsync(filter);
            return _player;

        }
        public async Task<Item> GetItem (Guid itemId, Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player _player = new Player();
            _player = await _db.GetCollection<Player>("Players").Find(filter).FirstOrDefaultAsync();
            
            foreach(Item i in _player.items){
                if(itemId == i.Id){
                    return i;
                }
            }
            return null;
        }
        public async Task<List<Item>> GetAllItems (Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player _player = new Player();
            _player = await _db.GetCollection<Player>("Players").Find(filter).FirstOrDefaultAsync();

            return _player.items;
        }
        public async Task<Item> CreateItem (Item item, Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player _player = new Player();
            _player = await _db.GetCollection<Player>("Players").Find(filter).FirstOrDefaultAsync();
            _player.items.Add(item);
            await _db.GetCollection<Player>("Players").DeleteOneAsync(filter);
            await _db.GetCollection<Player>("Players").InsertOneAsync(_player);
            return item;
        }

        public async Task<Item> ModifyItem (Guid itemId, ModifiedItem item, Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player _player = new Player();
            _player = await _db.GetCollection<Player>("Players").Find(filter).FirstOrDefaultAsync();

            foreach (Item i in _player.items)
            {
                if (i.Id == itemId)
                {
                    i.Type = item.Type;
                    i.Level = item.Level;
                }
            }
            await _db.GetCollection<Player>("Players").DeleteOneAsync(filter);
            await _db.GetCollection<Player>("Players").InsertOneAsync(_player); 
            return null;
        }
        public async Task<Item> DeleteItem (Guid itemId, Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player _player = new Player();
            _player = await _db.GetCollection<Player>("Players").Find(filter).FirstOrDefaultAsync();

            foreach (Item i in _player.items){
                if(itemId == i.Id){
                    _player.items.Remove(i);
                }
            }

            await _db.GetCollection<Player>("Players").DeleteOneAsync(filter);
            await _db.GetCollection<Player>("Players").InsertOneAsync(_player); 
            return null;
        }
    }
}