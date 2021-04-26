using Security.Models;

namespace Security.Storage
{
    public interface IPermissionStorage
    {
        public void Save(Permission permission);
        public Permission GetByName(string name);
        
        public Permission GetByExample(Permission permission);
        public void AddInnerPermission(Permission parent, Permission child);
    }
}