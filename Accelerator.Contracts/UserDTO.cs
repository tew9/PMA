using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Accelerator.Contracts
{
    /// <summary>
    /// User Data Transfer Object
    /// </summary>
    [DataContract]
    public class UserDTO
    {
        [BsonElement("_id")]
        [BsonRequired]
        [DataMember(Order = 0)]
        public string UserId { get; set; }
        [BsonElement("Email")]
        [DataMember(Order = 1)]
        public string Email { get; set; }
        [BsonElement("FirstName")]
        [DataMember(Order = 2)]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        [DataMember(Order = 3)]
        public string LastName { get; set; }
        [BsonElement("JobTitle")]
        [DataMember(Order = 4)]
        public string JobTitle { get; set; }
        [BsonElement("Phone")]
        [DataMember(Order = 5)]
        public string Phone { get; set; }
        
    }
}
