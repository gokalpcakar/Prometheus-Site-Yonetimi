using System;
using System.Collections.Generic;

#nullable disable

namespace Prometheus.DB.Entities
{
    public partial class User
    {
        public User()
        {
            Bill = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Tc { get; set; }
        public string PlateNo { get; set; }
        public DateTime Idate { get; set; }
        public DateTime? Udate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ApartmentId { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual ICollection<Bill> Bill { get; set; }
    }
}
