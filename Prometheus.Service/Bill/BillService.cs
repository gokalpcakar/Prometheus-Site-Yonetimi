using AutoMapper;
using Prometheus.DB.Entities.DataContext;
using Prometheus.Model;
using Prometheus.Model.Bill;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prometheus.Service.Bill
{
    public class BillService : IBillService
    {
        private readonly IMapper mapper;
        public BillService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public General<BillViewModel> GetById(int id)
        {
            var result = new General<BillViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var model = context.Bill.SingleOrDefault(i => i.Id == id);

                if (model is not null)
                {
                    result.Entity = mapper.Map<BillViewModel>(model);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Fatura başarıyla getirilmiştir.";
                }
                else
                {
                    result.ExceptionMessage = "Lütfen tekrar deneyiniz";
                }
            }

            return result;
        }
        public General<BillViewModel> GetBills()
        {
            var result = new General<BillViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var data = context.Bill.ToList();

                if (data.Any())
                {
                    result.List = mapper.Map<List<BillViewModel>>(data);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Faturalar başarıyla getirilmiştir.";
                }
                else
                {
                    result.ExceptionMessage = "Lütfen tekrar deneyiniz.";
                }
            }

            return result;
        }
        public General<BillViewModel> AddBill(AddBillViewModel newBill)
        {
            var result = new General<BillViewModel>() { IsSuccess = false };

            try
            {
                var model = mapper.Map<Prometheus.DB.Entities.Bill>(newBill);

                using (var context = new PrometheusContext())
                {
                    if (model is not null)
                    {
                        context.Bill.Add(model);
                        context.SaveChanges();

                        result.Entity = mapper.Map<BillViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Ekleme işlemi başarıyla gerçekleştirilmiştir.";
                    }
                    else
                    {
                        result.ExceptionMessage = "Lütfen tekrar deneyiniz.";
                    }
                }
            }
            catch
            {
                result.ExceptionMessage = "Beklenmedik bir hata oluştu.";
            }

            return result;
        }
    }
}
