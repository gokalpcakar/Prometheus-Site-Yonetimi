
namespace Prometheus.Model.Apartment
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }
        public string BlockName { get; set; }
        public bool IsFull { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentNo { get; set; }
        public int ApartmentFloor { get; set; }
        public bool IsDeleted { get; set; }
    }
}
