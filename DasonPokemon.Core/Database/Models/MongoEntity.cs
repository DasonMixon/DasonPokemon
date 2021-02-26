﻿using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MongoDB.Extensions.Repository.Models
{
    /// <summary>
    /// An entity in a MongoDB repository.
    /// </summary>
    public abstract class MongoEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }

        public string ExternalId { get; set; }
    }
}
