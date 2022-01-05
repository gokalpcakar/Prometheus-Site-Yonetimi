using Prometheus.Model;
using Prometheus.Model.Apartment;

namespace Prometheus.Service.Apartment
{
    public interface IApartmentService
    {
        public General<ApartmentViewModel> GetById(int id);
        public General<ApartmentViewModel> GetApartments();
        public General<ApartmentViewModel> AddApartment(ApartmentViewModel newApartment);
        public General<ApartmentViewModel> UpdateApartment(UpdateApartmentViewModel apartment);
    }
}
