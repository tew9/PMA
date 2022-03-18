using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.Contracts.Base
{
    public abstract class BaseEntityDTO
    {
        [BsonElement("_id")]
        [BsonRequired]
        public virtual string QID { get; set; }

        public void SetId(string id) =>
            QID = id;
    }
}
