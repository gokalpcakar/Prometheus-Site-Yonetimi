using Prometheus.Model;
using Prometheus.Model.Apartment;

namespace Prometheus.Service.Apartment
{
    public interface IApartmentService
    {
        public General<ApartmentViewModel> GetById(int id);
        public General<ApartmentViewModel> GetFullApartments();
        public General<ApartmentViewModel> GetEmptyApartments();
        public General<ApartmentViewModel> GetAllApartments();
        public General<ApartmentViewModel> AddApartment(ApartmentViewModel newApartment);
        public General<ApartmentViewModel> UpdateApartment(UpdateApartmentViewModel apartment);
        public General<ApartmentViewModel> DeleteApartment(int id);
    }
}
