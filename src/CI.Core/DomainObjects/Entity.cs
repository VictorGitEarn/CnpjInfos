using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CI.Core.DomainObjects
{
    public abstract class Entity
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
