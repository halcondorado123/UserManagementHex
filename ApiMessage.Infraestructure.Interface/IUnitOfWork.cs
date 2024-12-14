using ApiMessage.Data;

namespace ApiMessage.Infraestructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
    }
}
