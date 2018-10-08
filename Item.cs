using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTeht
{

    public class Item
    {
        public Guid Id { get; set; }
        public int Level {get; set;}
        public string Type {get; set;}
        public DateTime CreationTime {get; set;}
    }

    public class NewItem
    {


        [Range(0, 99)]
        public int Level { get; set; }
        [AllowedType]
        public string Type { get; set; }
    }

    public class ModifiedItem
    {
    
        [Range(0, 99)]
        public int Level { get; set; }
        [AllowedType]
        public string Type { get; set; }
    }
}