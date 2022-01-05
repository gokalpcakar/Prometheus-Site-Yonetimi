using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Model.Bill
{
    public class UpdateBillViewModel
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public decimal Price { get; set; }
        public DateTime? Udate { get; set; }
        public int UserId { get; set; }
    }
}
