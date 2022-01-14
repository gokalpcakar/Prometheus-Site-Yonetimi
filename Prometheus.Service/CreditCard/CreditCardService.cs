using MongoDB.Driver;
using Prometheus.DB.Database;
using System.Collections.Generic;

namespace Prometheus.Service.CreditCard
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IMongoCollection<Prometheus.DB.Entities.CreditCard> creditCards;
        public CreditCardService(IDbClient dbClient)
        {
            creditCards = dbClient.GetCreditCardsCollection();
        }
        public List<Prometheus.DB.Entities.CreditCard> GetCreditCards()
        {
            return creditCards.Find(creditCard => true).ToList();
        }
        public Prometheus.DB.Entities.CreditCard AddCreditCard(Prometheus.DB.Entities.CreditCard newCreditCard)
        {
            creditCards.InsertOne(newCreditCard);
            return newCreditCard;
        }
        public Prometheus.DB.Entities.CreditCard GetCreditCard(string id)
        {
            return creditCards.Find(creditCard => creditCard.Id == id).First();
        }
        public void DeleteCreditCard(string id)
        {
            creditCards.DeleteOne(creditCard => creditCard.Id == id);
        }
        public Prometheus.DB.Entities.CreditCard UpdateCreditCard(Prometheus.DB.Entities.CreditCard creditCard)
        {
            GetCreditCard(creditCard.Id);
            creditCards.ReplaceOne(b => b.Id == creditCard.Id, creditCard);
            return creditCard;
        }
    }
}
