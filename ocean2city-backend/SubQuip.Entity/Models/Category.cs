using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ocean2City.Entity.Models
{
    public class Category
    {
        [BsonId]
        public ObjectId CategoryId { get; set; }

        [BsonElement("categoryName")]
        public string CategoryName { get; set; }

        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }
    }
}
