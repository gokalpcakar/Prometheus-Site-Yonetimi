using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Prometheus.DB.Entities
{
    public class CreditCard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CreditCardNo { get; set; }
        public string CreditCardCVV { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
