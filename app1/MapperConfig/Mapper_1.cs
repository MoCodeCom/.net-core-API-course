using app1.DTO;
using app1.Model;
using AutoMapper;

namespace app1.MapperConfig
{
    public class Mapper_1 : Profile
    {
        public Mapper_1()
        {
            //build relationship mapper
            CreateMap<VillaModel, VillaModelDTO>().ReverseMap();
            //To reverse relationship
            CreateMap<VillaModelDTO, VillaModel>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumber>().ReverseMap();
            CreateMap<CreateVillaNumberDTO, VillaNumber>().ReverseMap();
            CreateMap<DeleteVillaNumberDTO, VillaNumber>().ReverseMap();
            CreateMap<UpdateVillaNumberDTO, VillaNumber>().ReverseMap();


            //to build map and reverse the relationship in same time.
            CreateMap<VillaModel, CreateVillaModelDTO>().ReverseMap();
        }
    }
}
