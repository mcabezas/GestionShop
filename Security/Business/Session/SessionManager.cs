using Security.Storage;

namespace Security.Business.Session
{
    public class SessionManager : ISessionManager
    {
        private readonly ISessionStorage _sessionStorage;

        public SessionManager(ISessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public void Create(Models.Session session)
        {
            _sessionStorage.Create(session);
        }

        public Models.Session Get(string hash)
        {
            return _sessionStorage.Get(hash);
        }
    }
}