using Security.Models;

namespace Security.Storage
{
    public interface ISessionStorage
    {
        public void Create(Session session);
        public Session Get(string hash);
    }
}