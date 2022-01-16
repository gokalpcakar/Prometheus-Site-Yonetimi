
namespace Prometheus.Model.User
{
    public class ApartmentUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ApartmentId { get; set; }
        public string BlockName { get; set; }
        public bool IsFull { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentNo { get; set; }
        public int ApartmentFloor { get; set; }
    }
}
