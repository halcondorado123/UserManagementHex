using ApiMessage.Data;

namespace ApiMessage.Infraestructure.Repository
{
    public class UnitOfWork
    {
         public IUserRepository Users { get; }

        public UnitOfWork(IUserRepository users)
        {
            Users = users;
        }
        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
