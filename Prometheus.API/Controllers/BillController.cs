using Microsoft.AspNetCore.Mvc;
using Prometheus.DB.Entities.DataContext;
using Prometheus.Model;
using Prometheus.Model.Bill;
using Prometheus.Service.Bill;
using System.Linq;

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

        // id'ye göre fatura ya da aidat'ın ödeme bilgisi güncelleniyor
        [HttpPut("{id}")]
        public General<BillViewModel> PayBill(int id)
        {
            return billService.PayBill(id);
        }

        [HttpGet("UserBills")]
        public ActionResult GetUserBills()
        {
            using (var context = new PrometheusContext())
            {
                // user ve bill tabloları join edilerek fatura-aidat bilgileri ile kullanıcı bilgilerini beraber alıyoruz
                var query = from bill in context.Bill
                            join user in context.User
                            on bill.UserId equals user.Id
                            where user.Id == bill.UserId && !bill.IsPaid && !bill.IsDeleted && !user.IsDeleted
                            orderby bill.Idate
                            select new UserBillViewModel()
                            {
                                Id = bill.Id,
                                BillType = bill.BillType,
                                Price = bill.Price,
                                Idate = bill.Idate,
                                DueDate = bill.DueDate,
                                Name = user.Name,
                                Surname = user.Surname,
                                UserId = user.Id,
                            };

                var list = query.ToList();

                return Ok(list);
            }
        }

        [HttpGet("{id}")]
        public General<BillViewModel> GetById(int id)
        {
            return billService.GetById(id);
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
