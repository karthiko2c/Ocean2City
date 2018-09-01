using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Entity.Models
{
    public class User
    {
        [BsonId]
        public ObjectId UserId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("mobNo")]
        public string MobNo { get; set; }

        [BsonElement("mailId")]
        public string MailId { get; set; }

        [BsonElement("userAddress")]
        public List<UserAddress> UserAddress { get; set; }
    }
}
