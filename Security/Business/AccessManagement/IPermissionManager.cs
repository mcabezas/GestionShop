using Security.Models;

namespace Security.Business.AccessManagement
{
    public interface IPermissionManager
    {
        public void CreatePermission(Permission permission);
        public Node AddInnerPermission(Node parent, Permission child);

        public Node BanInnerPermission(Node parent, Permission child);
        public Permission GetPermissionByName(string permissionName);
    }
}