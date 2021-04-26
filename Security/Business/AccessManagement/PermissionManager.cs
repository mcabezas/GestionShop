using Libs.Exceptions;
using Security.Models;
using Security.Storage;

namespace Security.Business.AccessManagement
{
    public class PermissionManager : IPermissionManager
    {
        private readonly IPermissionStorage _permissionStorage;

        public PermissionManager(IPermissionStorage permissionStorage)
        {
            _permissionStorage = permissionStorage;
        }

        public void CreatePermission(Permission permission)
        {
            _permissionStorage.Save(permission);
        }

        public Node AddInnerPermission(Node parent, Permission child)
        {
            CheckInnerPermissions(parent, child);
            CheckRecursiveDeadLock(parent, child);
            _permissionStorage.AddInnerPermission(parent, child);
            var updatedPermissions = parent.GetChildren().Add(child);
            return new Node(parent.Name, parent.Description, updatedPermissions, false);
        }

        public Node BanInnerPermission(Node parent, Permission child)
        {
            CheckInnerPermissions(parent, child);
            child = child.Ban();
            _permissionStorage.AddInnerPermission(parent, child);
            var updatedPermissions = parent.GetChildren().Add(child);
            return new Node(parent.Name, parent.Description, updatedPermissions, false);
        }

        public Permission GetPermissionByName(string permissionName)
        {
            return _permissionStorage.GetByName(permissionName);
        }

        private void CheckInnerPermissions(Node parent, Permission child)
        {
            if (parent.Equals(child))
            {
                throw new InvalidArgumentException("A permission cannot contains itself :( ");
            }
        }
        
        private void CheckRecursiveDeadLock(Node parent, Permission child)
        {
            var byExample = _permissionStorage.GetByExample(child);
            if (byExample == null) { return; }
            if (byExample.Contains(parent))
            {
                throw new InvalidArgumentException("Recurse assignment detected. The child does already contains the parent");
            }
        }
    }
}