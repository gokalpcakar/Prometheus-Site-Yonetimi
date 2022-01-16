using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Model.Bill
{
    // join işlemi gerçekleştirildiğinde iki tablonun verisini listlemek için bu model kullanılıyor
    public class UserBillViewModel
    {
        public int Id { get; set; }

        // Kullanıcının adı
        public string Name { get; set; }

        // Kullanıcının soyadı
        public string Surname { get; set; }
        public string BillType { get; set; }
        public decimal Price { get; set; }
        public DateTime Idate { get; set; }
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
    }
}
