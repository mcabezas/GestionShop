using System.Collections.Generic;
using Security.Business.AccessManagement;
using Security.Business.Session;

namespace Security.Business.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IIdentityManager _identityManager;
        private readonly ISessionManager _sessionManager;

        public AuthorizationManager(IIdentityManager identityManager, ISessionManager sessionManager)
        {
            _identityManager = identityManager;
            _sessionManager = sessionManager;
        }

        public bool Authorize(Models.Session session, string permissionName)
        {
            var sess = _sessionManager.Get(session.Hash);
            if (sess == null) return false;

            var identity = _identityManager.Get(session.Username);
            var negatives = new HashSet<string>();

            var hasPermission = false;
            foreach (var leaf in identity.Permission.GetLeaves())
            {
                if (leaf.Banned)
                {
                    negatives.Add(leaf.Name);
                }

                if (leaf.Name == permissionName)
                {
                    hasPermission = true;
                }
            }

            return hasPermission && !negatives.Contains(permissionName);
        }
    }
}