using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouffonChatCLI.Models
{
    [BsonIgnoreExtraElements]
    public class Profile
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("googleId")]
        public string GoogleId { get; set; } = "";

        [BsonElement("name")]
        public string Name { get; set; } = "";

        [BsonElement("email")]
        public string Email { get; set; } = "";

        [BsonElement("defaultInstructions")]
        public string DefaultInstructions { get; set; } = "";

        [BsonElement("defaultTone")]
        public string DefaultTone { get; set; } = "";

        [BsonElement("defaultModel")]
        public string DefaultModel { get; set; } = "";

        [BsonElement("refreshToken")]
        public string RefreshToken { get; set; } = "";

        [BsonElement("status")]
        public string Status { get; set; } = "inactive";
    }
}
