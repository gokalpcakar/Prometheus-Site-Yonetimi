using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prometheus.DB.Entities.DataContext;
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

        [HttpGet("GetBillsForUser/{id}")]
        public General<BillViewModel> GetBillsForUser(int id)
        {
            return billService.GetBillsForUser(id);
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
