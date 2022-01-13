
using System.ComponentModel.DataAnnotations;

namespace Prometheus.Model.User
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Tc { get; set; }
        public string PlateNo { get; set; }
        public int ApartmentId { get; set; }
    }
}
