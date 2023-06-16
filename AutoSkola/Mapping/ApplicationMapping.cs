using AutoMapper;
using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Contracts.Models.Kategorija;
using AutoSkola.Data.Models;

namespace AutoSkola.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<Kategorija, CreateKategorijaResponse>();
        }
    }
}
