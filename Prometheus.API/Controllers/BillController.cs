using Microsoft.AspNetCore.Mvc;
using Prometheus.Model;
using Prometheus.Model.Bill;
using Prometheus.Service.Bill;

namespace Prometheus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService billService;
        public BillController(IBillService _billService)
        {
            billService = _billService;
        }

        [HttpGet("GetUnpaidBillsForUser/{id}")]
        public General<BillViewModel> GetUnpaidBillsForUser(int id)
        {
            return billService.GetUnpaidBillsForUser(id);
        }

        [HttpGet("GetPaidBillsForUser/{id}")]
        public General<BillViewModel> GetPaidBillsForUser(int id)
        {
            return billService.GetPaidBillsForUser(id);
        }

        [HttpGet("GetBillsForUser/{id}")]
        public General<BillViewModel> GetBillsForUser(int id)
        {
            return billService.GetBillsForUser(id);
        }

        [HttpPut("{id}")]
        public General<BillViewModel> PayBill(int id)
        {
            return billService.PayBill(id);
        }

        [HttpGet("{id}")]
        public General<BillViewModel> GetById(int id)
        {
            return billService.GetById(id);
        }

        [HttpGet]
        public General<BillViewModel> GetAllBills()
        {
            return billService.GetAllBills();
        }

        [Route("GetUnpaidBills")]
        [HttpGet]
        public General<BillViewModel> GetUnpaidBills()
        {
            return billService.GetUnpaidBills();
        }

        [HttpPost]
        public General<BillViewModel> AddBill(AddBillViewModel newBill)
        {
            return billService.AddBill(newBill);
        }

        [HttpPut]
        public General<BillViewModel> UpdateBill(UpdateBillViewModel bill)
        {
            return billService.UpdateBill(bill);
        }

        [HttpDelete("{id}")]
        public General<BillViewModel> DeleteBill(int id)
        {
            return billService.DeleteBill(id);
        }
    }
}
