using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
        public General<BillViewModel> GetById(int id)
        {
            return billService.GetById(id);
        }

        [HttpGet]
        public General<BillViewModel> GetBills()
        {
            return billService.GetBills();
        }

        [HttpPost]
        public General<BillViewModel> AddBill(AddBillViewModel newBill)
        {
            return billService.AddBill(newBill);
        }
    }
}
