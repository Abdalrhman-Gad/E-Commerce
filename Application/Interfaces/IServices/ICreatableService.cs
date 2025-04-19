namespace Application.Interfaces.IServices
{
    public interface ICreatableService<TAdd, TGet>
    {
        Task<TGet> AddAsync(TAdd entity);
    }
}
