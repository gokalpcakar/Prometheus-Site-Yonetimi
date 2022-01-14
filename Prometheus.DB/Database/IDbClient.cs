using MongoDB.Driver;
using Prometheus.DB.Entities;

namespace Prometheus.DB.Database
{
    public interface IDbClient
    {
        IMongoCollection<CreditCard> GetCreditCardsCollection();
    }
}
