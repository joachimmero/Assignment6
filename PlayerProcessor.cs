using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ApiTeht{

    public class PlayerProcessor{
        IRepository _IRepository;

        public PlayerProcessor(IRepository repo){
            this._IRepository = repo;
        }

        public Task<Player> Get(Guid id){
            return _IRepository.Get(id);
        }
        public Task<List<Player>> GetAll(){
            return _IRepository.GetAll();
        }
        public Task<Player> Create(NewPlayer player){
            Player _player = new Player();
            _player.Id = Guid.NewGuid();
            _player.Name = player.Name;
            _player.Score = 0;
            _player.IsBanned = false;
            _player.CreationTime = DateTime.Now;

            return _IRepository.Create(_player);
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            return _IRepository.Modify(id, player);
        }
        public Task<Player> Delete(Guid id){
            return _IRepository.Delete(id);
        }
    }
}