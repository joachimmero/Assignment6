using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTeht
{
    public class ItemsProcessor
    {
        IRepository _IRepository;

        public ItemsProcessor(IRepository iRepository){
            this._IRepository = iRepository;
        }
        public Task<Item> Get(Guid id, Guid playerId){

            return _IRepository.GetItem(id, playerId);
        }
        public Task<List<Item>> GetAllItems(Guid playerId){

            return _IRepository.GetAllItems(playerId);
        }

        public Task<Item> Create(NewItem newItem, Guid playerId){
            
            
            if(newItem.Level < 98){
                throw new CustomException();
            }
            
            Item _item = new Item();
            _item.Type = newItem.Type;
            _item.Level = newItem.Level;
            _item.CreationTime = DateTime.Now;
            _item.Id = Guid.NewGuid();
            return _IRepository.CreateItem(_item, playerId);
        }
        public Task<Item> Modify(Guid id, ModifiedItem item, Guid playerId){

            return _IRepository.ModifyItem(id, item, playerId);
        }
        public Task<Item> Delete(Guid id, Guid playerId){
            return _IRepository.DeleteItem(id, playerId);
        }
    }
}