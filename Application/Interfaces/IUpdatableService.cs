﻿namespace Application.Interfaces
{
    public interface IUpdatableService<TGet, TUpdate>
        where TGet : class
        where TUpdate : class
    {
        Task<TGet> UpdateAsync(int id, TUpdate entity);
    }
}