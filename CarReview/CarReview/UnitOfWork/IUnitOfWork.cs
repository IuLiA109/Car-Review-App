namespace CarReview.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveAsync();
    }
}
