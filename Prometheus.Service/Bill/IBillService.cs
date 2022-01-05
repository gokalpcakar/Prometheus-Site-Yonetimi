using Prometheus.Model;
using Prometheus.Model.Bill;

namespace Prometheus.Service.Bill
{
    public interface IBillService
    {
        public General<BillViewModel> GetById(int id);
        public General<BillViewModel> GetAllBills();
        public General<BillViewModel> GetUnpaidBills();
        public General<BillViewModel> AddBill(AddBillViewModel newBill);
        public General<BillViewModel> UpdateBill(UpdateBillViewModel bill);
        public General<BillViewModel> DeleteBill(int id);
    }
}
