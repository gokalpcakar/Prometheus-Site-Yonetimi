using AutoMapper;
using Prometheus.DB.Entities.DataContext;
using Prometheus.Model;
using Prometheus.Model.Apartment;
using System.Collections.Generic;
using System.Linq;

namespace Prometheus.Service.Apartment
{
    public class ApartmentService : IApartmentService
    {
        private readonly IMapper mapper;
        public ApartmentService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public General<ApartmentViewModel> GetById(int id)
        {
            var result = new General<ApartmentViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var model = context.Apartment.SingleOrDefault(i => i.Id == id);

                if (model is not null)
                {
                    result.Entity = mapper.Map<ApartmentViewModel>(model);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Konut bilgisi başarıyla getirilmiştir.";
                }
                else
                {
                    result.ExceptionMessage = "Lütfen tekrar deneyiniz";
                }
            }

            return result;
        }
        public General<ApartmentViewModel> GetApartments()
        {
            var result = new General<ApartmentViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var model = context.Apartment.OrderBy(i => i.Id);

                if (model is not null)
                {
                    result.List = mapper.Map<List<ApartmentViewModel>>(model);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Konutlar başarıyla getirildi.";
                }
                else
                {
                    result.ExceptionMessage = "Lütfen tekrar giriniz.";
                }
            }

            return result;
        }
        public General<ApartmentViewModel> AddApartment(ApartmentViewModel newApartment)
        {

            var result = new General<ApartmentViewModel>() { IsSuccess = false };

            try
            {
                using (var context = new PrometheusContext())
                {
                    var model = mapper.Map<Prometheus.DB.Entities.Apartment>(newApartment);

                    if (model is not null)
                    {
                        context.Apartment.Add(model);
                        context.SaveChanges();

                        result.Entity = mapper.Map<ApartmentViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Konut başarıyla eklendi.";
                    }
                    else
                    {
                        result.ExceptionMessage = "Lütfen tekrar giriniz.";
                    }
                }
            }
            catch
            {
                result.ExceptionMessage = "Beklenmedik bir hata oluştu";
            }

            return result;
        }
        public General<ApartmentViewModel> UpdateApartment(UpdateApartmentViewModel apartment)
        {
            var result = new General<ApartmentViewModel>() { IsSuccess = false };

            try
            {
                using (var context = new PrometheusContext())
                {
                    var model = context.Apartment.SingleOrDefault(i => i.Id == apartment.Id);

                    if (model is not null)
                    {
                        model.BlockName = apartment.BlockName;
                        model.ApartmentType = apartment.ApartmentType;
                        model.IsFull = apartment.IsFull;
                        model.ApartmentNo = apartment.ApartmentNo;
                        model.ApartmentFloor = apartment.ApartmentFloor;

                        context.SaveChanges();

                        result.Entity = mapper.Map<ApartmentViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Konut güncellemesi başarıyla gerçekleştirilmiştir.";
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
