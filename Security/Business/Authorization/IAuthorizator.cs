namespace Security.Business.Authorization
{
    public interface IAuthorizationManager
    {
        public bool Authorize(Models.Session session, string permissionName);
    }
}