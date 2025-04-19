using AutoMapper;

namespace Application.Mappings.BaseProfiles
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