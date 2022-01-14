using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Service.CreditCard
{
    public interface ICreditCardService
    {
        public List<Prometheus.DB.Entities.CreditCard> GetCreditCards();
        public Prometheus.DB.Entities.CreditCard AddCreditCard(Prometheus.DB.Entities.CreditCard newCreditCard);
        public Prometheus.DB.Entities.CreditCard GetCreditCard(string id);
        public void DeleteCreditCard(string id);
        public Prometheus.DB.Entities.CreditCard UpdateCreditCard(Prometheus.DB.Entities.CreditCard creditCard);
    }
}
