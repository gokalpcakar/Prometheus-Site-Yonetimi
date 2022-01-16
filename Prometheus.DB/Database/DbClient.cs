using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Prometheus.DB.Entities;
using System;

namespace Prometheus.DB.Database
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<CreditCard> creditCards;
        public DbClient(IOptions<CreditCardDbConfig> creditCardDbConfig)
        {
            var client = new MongoClient(creditCardDbConfig.Value.Connection_String);
            var database = client.GetDatabase(creditCardDbConfig.Value.Database_Name);
            creditCards = database.GetCollection<CreditCard>(creditCardDbConfig.Value.Credit_Card_Collection_Name);
        }
        // kredi kartlarını almamızı sağlıyor
        public IMongoCollection<CreditCard> GetCreditCardsCollection()
        {
            return creditCards;
        }
    }
}
