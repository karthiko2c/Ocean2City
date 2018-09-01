using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SubQuip.Entity;
using System;
using System.Collections.Generic;

namespace Ocean2City.Entity.Models
{
    public class OrderDetail: BaseEntity
    {
        [BsonId]
        public ObjectId OrderId { get; set; }

        [BsonElement("userId")]
        public ObjectId UserId { get; set; }

        [BsonElement("addressId")]
        public ObjectId AddressId { get; set; }

        [BsonElement("deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [BsonElement("deliveryTiming")]
        public string DeliveryTiming { get; set; }

        [BsonElement("isDelivered")]
        public bool IsDelivered { get; set; }

        [BsonElement("totalAmount")]
        public double TotalAmount { get; set; }

        [BsonElement("cartItems")]
        public List<CartItem> CartItems { get; set; }
    }
}
