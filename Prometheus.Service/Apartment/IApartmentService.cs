using Prometheus.Model;
using Prometheus.Model.Apartment;

namespace Prometheus.Service.Apartment
{
    public interface IApartmentService
    {
        public General<ApartmentViewModel> GetById(int id);
        public General<ApartmentViewModel> GetEmptyApartments();
        public General<ApartmentViewModel> GetAllApartments();
        public General<ApartmentViewModel> AddApartment(AddApartmentViewModel newApartment);
        public General<ApartmentViewModel> UpdateApartment(UpdateApartmentViewModel apartment);
        public General<ApartmentViewModel> DeleteApartment(int id);
    }
}
