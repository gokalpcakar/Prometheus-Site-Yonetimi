using Prometheus.Model;
using Prometheus.Model.Bill;

namespace Prometheus.Service.Bill
{
    public interface IBillService
    {
        public General<BillViewModel> GetUnpaidBillsForUser(int id);
        public General<BillViewModel> GetPaidBillsForUser(int id);
        public General<BillViewModel> PayBill(int id);
        public General<BillViewModel> GetById(int id);
        public General<BillViewModel> AddBill(AddBillViewModel newBill);
        public General<BillViewModel> UpdateBill(UpdateBillViewModel bill);
        public General<BillViewModel> DeleteBill(int id);
    }
}
