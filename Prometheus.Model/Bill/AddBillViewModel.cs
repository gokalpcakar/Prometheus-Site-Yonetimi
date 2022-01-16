using System;

namespace Prometheus.Model.Bill
{
    public class AddBillViewModel
    {
        public string BillType { get; set; }
        public decimal Price { get; set; }
        public DateTime Idate { get; set; }
        public int UserId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
