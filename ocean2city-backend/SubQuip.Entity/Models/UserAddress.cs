using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Entity.Models
{
    public class UserAddress
    {
        [BsonId]
        public ObjectId AddressId { get; set; }

        [BsonElement("completeAddress")]
        public string CompleteAddress { get; set; }

        [BsonElement("landmark")]
        public string Landmark { get; set; }

        [BsonElement("deliveryInstructions")]
        public string DeliveryInstruction { get; set; }

        [BsonElement("pincode")]
        public string PinCode { get; set; }
    }
}
