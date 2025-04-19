using Application.Mappings.BaseProfiles;

public abstract class BaseMappingProfileWithUpdate<T, TAdd, TUpdate, TGet> : 
    BaseMappingProfile<T, TAdd, TGet>
{
    protected BaseMappingProfileWithUpdate()
    {
        CreateMap<TUpdate, T>();
    }
}