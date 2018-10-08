using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTeht
{

    public interface IRepository
    {

        Task DeleteStarted();
        Task DeleteSuccess();

        Task<Player> Get (Guid id);
        Task<List<Player>> GetAll ();
        Task<Player> Create (Player player);
        Task<Player> Modify (Guid id, ModifiedPlayer player);
        Task<Player> Delete (Guid id);

        Task<Item> GetItem (Guid itemId, Guid playerId);
        Task<List<Item>> GetAllItems (Guid playerId);
        Task<Item> CreateItem (Item item, Guid playerId);
        Task<Item> ModifyItem (Guid itemId, ModifiedItem item, Guid playerId);
        Task<Item> DeleteItem (Guid itemId, Guid playerId);

    }

    public class InMemoryRepository : IRepository
    {
        List<Player> _players = new List<Player> ();
        // Dictionary<Player, List<Item>> items = new Dictionary<Player, List<Item>> ();
        public InMemoryRepository ()
        {

        }
        
        private List<LogEntry> Log = new List<LogEntry>();

        public Task DeleteStarted()
        {
            Log.Add(new LogEntry("A request to delete player started at " + DateTime.Now.ToString()));
            return Task.CompletedTask;
        }
        
        public Task DeleteSuccess()
        {
            Log.Add(new LogEntry("A request to delete player ended at " + DateTime.Now.ToString()));
            return Task.CompletedTask;
        }
        public async Task<Player> Get (Guid id)
        {
            if (_players.Count > 0)
            {
                foreach (Player p in _players)
                {
                    if (p.Id == id)
                    {
                        return p;
                    }

                }
            }
            else
            {
                return null;
            }
            return null;
        }

        public async Task<List<Player>> GetAll ()
        {
            return _players;
        }

        public async Task<Player> Create (Player player)
        {
            Player _player = new Player ();
            _player = player;
            _players.Add (_player);
            return _player;
        }

        public async Task<Player> Modify (Guid id, ModifiedPlayer player)
        {
            if (_players.Count > 0)
            {
                foreach (Player p in _players)
                {
                    if (p.Id == id)
                    {
                        p.Score = player.Score;
                        return p;
                    }
                }
            }
            else
            {
                return null;
            }
            return null;
        }
        public async Task<Player> Delete (Guid id)
        {
            foreach (Player p in _players)
            {
                if (p.Id == id)
                {
                    _players.Remove (p);
                    return p;
                }
            }
            return null;
        }

        public async Task<Item> GetItem (Guid itemId, Guid playerId)
        {
            if (_players.Count > 0)
            {
                foreach (Player p in _players)
                {
                    if (p.Id == playerId)
                    {
                        if (p.items.Count > 0)
                        {
                            foreach (Item i in p.items)
                            {
                                if (i.Id == itemId)
                                {
                                    return i;
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                return null;
            }
            return null;
        }
        public async Task<List<Item>> GetAllItems (Guid playerId)
        {
            if (_players.Count > 0)
            {
                foreach (Player p in _players)
                {
                    if (p.Id == playerId)
                    {
                        return p.items;
                    }
                }
            }
            return null;
        }
        public async Task<Item> CreateItem (Item item, Guid playerId)
        {
            if (_players.Count > 0)
            {
                foreach (Player p in _players)
                {
                    if (p.Id == playerId)
                    {
                        p.items.Add(item);
                        return item;
                    }
                }
            }
            return null;
        }

        public async Task<Item> ModifyItem (Guid itemId, ModifiedItem item, Guid playerId)
        {
            if (_players.Count > 0)
            {
                foreach (Player p in _players)
                {
                    if (p.Id == playerId)
                    {
                        if (p.items.Count > 0)
                        {
                            foreach (Item i in p.items)
                            {
                                if (i.Id == itemId)
                                {
                                    i.Type = item.Type;
                                    i.Level = item.Level;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
        public async Task<Item> DeleteItem (Guid itemId, Guid playerId)
        {
            if (_players.Count > 0)
            {
                foreach (Player p in _players)
                {
                    if (p.Id == playerId)
                    {
                        if (p.items.Count > 0)
                        {
                            foreach (Item item in p.items)
                            {
                                if (item.Id == itemId)
                                {
                                    p.items.Remove (item);
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

    }

}