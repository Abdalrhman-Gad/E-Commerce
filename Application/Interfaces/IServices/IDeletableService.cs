namespace Application.Interfaces.IServices
{
    public interface IDeletableService<TId>
    {
        Task DeleteAsync(TId id);
    }
}