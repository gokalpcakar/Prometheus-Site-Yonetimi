using System;
using System.Collections.Generic;

#nullable disable

namespace Prometheus.DB.Entities
{
    public partial class Apartment
    {
        public Apartment()
        {
            Admin = new HashSet<Admin>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string BlockName { get; set; }
        public bool IsFull { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentNo { get; set; }
        public int ApartmentFloor { get; set; }

        public virtual ICollection<Admin> Admin { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
