namespace Security.Business.Session
{
    public interface ISessionManager
    {
        public void Create(Models.Session session);
        public Models.Session Get(string hash);
    }
}