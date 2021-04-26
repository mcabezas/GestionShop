using Security.Models;

namespace Security.Storage
{
    public interface IIdentityStorage
    {
        public void Save(Identity identity);
        public Identity GetByName(string username);
        public void UpdatePermission(Identity identity);
    }
}