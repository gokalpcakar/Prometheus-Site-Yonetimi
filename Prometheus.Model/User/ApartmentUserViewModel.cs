
namespace Prometheus.Model.User
{
    // konut bilgileri ile kullanıcı bilgilerini bir arada almamızı sağlayan viewmodel
    public class ApartmentUserViewModel
    {
        public int Id { get; set; }

        // kullanıcının adı
        public string Name { get; set; }

        //kullanıcının soyadı
        public string Surname { get; set; }
        public int ApartmentId { get; set; }
        public string BlockName { get; set; }
        public bool IsFull { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentNo { get; set; }
        public int ApartmentFloor { get; set; }
    }
}
