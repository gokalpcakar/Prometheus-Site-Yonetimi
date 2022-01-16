using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Model.Apartment
{
    public class AddApartmentViewModel
    {
        public string BlockName { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentNo { get; set; }
        public int ApartmentFloor { get; set; }
    }
}
