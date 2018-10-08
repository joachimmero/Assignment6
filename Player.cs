using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiTeht
{
    public class Player
    {
        public ObjectId objectId;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Item> items { get; set; }

        public Player(){
            items = new List<Item>();
        }
    }

    public class NewPlayer
    {
        public string Name { get; set; }
    }

    public class ModifiedPlayer
    {
        public int Score { get; set; }
    }
}