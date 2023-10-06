namespace WindowsFormsAppWithFirebird.Domain.Data
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
