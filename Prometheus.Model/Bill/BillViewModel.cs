using System;

namespace Prometheus.Model.Bill
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public decimal Price { get; set; }
        public DateTime Idate { get; set; }
        public DateTime? Udate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
    }
}
