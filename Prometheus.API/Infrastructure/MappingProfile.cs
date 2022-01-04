using AutoMapper;
using Prometheus.DB.Entities;
using Prometheus.Model.User;

namespace Prometheus.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        // Hangi DB class'ı ile hangi ViewModel'in map'leneceğini ayarladığımız kısım
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<User, AddUserViewModel>();
            CreateMap<AddUserViewModel, User>();
        }
    }
}
