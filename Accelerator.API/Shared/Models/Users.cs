using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.API.Shared.Models
{
    public class Users
    {
        public string UserId { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("JobTitle")]
        public string JobTitle { get; set; }
        [JsonProperty("Phone")]
        public string Phone { get; set; }
    }
}
