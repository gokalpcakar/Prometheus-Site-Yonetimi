using Prometheus.Model;
using Prometheus.Model.Bill;

namespace Prometheus.Service.Bill
{
    public interface IBillService
    {
        public General<BillViewModel> GetById(int id);
        public General<BillViewModel> GetBills();
        public General<BillViewModel> AddBill(AddBillViewModel newBill);
    }
}
