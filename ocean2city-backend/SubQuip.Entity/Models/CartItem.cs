using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Entity.Models
{
    public class CartItem
    {
        [BsonId]
        public ObjectId CartId { get; set; }

        [BsonElement("categoryId")]
        public ObjectId CategoryId { get; set; }
        
        [BsonElement("itemId")]
        public ObjectId ItemId { get; set; }

        [BsonElement("itemQty")]
        public int ItemQuantity { get; set; }

        [BsonElement("isCleaned")]
        public bool IsCleaned { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }
    }
}
