using AutoMapper;

namespace Application.Mappings
{
    public abstract class BaseMappingProfile<T, TAdd, TGet> : Profile
    {
        protected BaseMappingProfile()
        {
            CreateMap<TAdd, T>();
           
            CreateMap<T, TGet>();
        }
    }
}