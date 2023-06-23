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
            CreateMap<CreateKategorijaResponse,Kategorija>();
            CreateMap<RegisterRequest, Kategorija>();
            CreateMap<Kategorija, RegisterRequest>();
            CreateMap<UserResponse, Kategorija>();
            CreateMap<Kategorija, UserResponse>();
            CreateMap<RegisterRequest, User>()
   .ForMember(dest => dest.userkategorija, opt => opt.MapFrom(src => new List<UserKategorija> { new UserKategorija { KategorijaId = src.kategorijaId } }));
            CreateMap<User, UserResponse>()
           .ForMember(dest => dest.kategorijaId, opt => opt.MapFrom(src => src.userkategorija.FirstOrDefault().KategorijaId));
           
        }
    }
}
