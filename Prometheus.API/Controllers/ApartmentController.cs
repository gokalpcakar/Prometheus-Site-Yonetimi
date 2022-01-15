using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Model;
using Prometheus.Model.Apartment;
using Prometheus.Service.Apartment;

namespace Prometheus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService apartmentService;
        public ApartmentController(IApartmentService _apartmentService)
        {
            apartmentService = _apartmentService;
        }
        [HttpGet("{id}")]
        public General<ApartmentViewModel> GetById(int id)
        {
            return apartmentService.GetById(id);
        }
        [HttpGet]
        public General<ApartmentViewModel> GetAllApartments()
        {
            return apartmentService.GetAllApartments();
        }
        [Route("FullApartments")]
        [HttpGet]
        public General<ApartmentViewModel> GetFullApartments()
        {
            return apartmentService.GetFullApartments();
        }
        [Route("EmptyApartments")]
        [HttpGet]
        public General<ApartmentViewModel> GetEmptyApartments()
        {
            return apartmentService.GetEmptyApartments();
        }
        [HttpPost]
        public General<ApartmentViewModel> AddApartment(ApartmentViewModel newApartment)
        {
            return apartmentService.AddApartment(newApartment);
        }
        [HttpPut]
        public General<ApartmentViewModel> UpdateApartment(UpdateApartmentViewModel apartment)
        {
            return apartmentService.UpdateApartment(apartment);
        }
        [HttpDelete("{id}")]
        public General<ApartmentViewModel> DeleteApartment(int id)
        {
            return apartmentService.DeleteApartment(id);
        }
    }
}
