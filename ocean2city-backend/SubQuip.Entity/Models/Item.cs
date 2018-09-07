using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SubQuip.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Entity.Models
{
    public class Item: BaseEntity
    {
        [BsonId]
        public ObjectId ItemId { get; set; }
        
        [BsonElement("itemName")]
        public string ItemName { get; set; }

        [BsonElement("aliasName")]
        public string AliasName { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("recipe")]
        public string Recipe { get; set; }

        [BsonElement("priceWithoutClean")]
        public double PriceWithoutClean { get; set; }

        [BsonElement("priceWithClean")]
        public double PriceWithClean { get; set; }

        [BsonElement("discount")]
        public int Discount { get; set; }
        
        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }

        [BsonElement("category")]
        public ObjectId Category { get; set; }
    }
}
