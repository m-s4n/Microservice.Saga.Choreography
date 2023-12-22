﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Stock.Service.DataAccess.Entities
{
    public class Stock
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement(Order = 1)]
        public int ProductId { get; set; }
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement(Order = 2)]
        public int Count { get; set; }
    }
}
