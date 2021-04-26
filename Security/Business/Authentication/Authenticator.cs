using System;
using System.Text;
using Libs.Common;
using Security.Business.Session;
using Security.Storage;

namespace Security.Business.Authentication
{
    public class AuthenticationManager
    {
        private readonly IIdentityStorage _identityStorage;
        private readonly ISessionManager _sessionManager;

        public AuthenticationManager(IIdentityStorage identityStorage, ISessionManager sessionManager)
        {
            _identityStorage = identityStorage;
            _sessionManager = sessionManager;
        }

        public Models.Session Authenticate(string username, string credentials)
        {
            var credentialsBytes = Encoding.UTF8.GetBytes(credentials);
            
            // fast credentials clear to avoid any kind of memory sniff
            credentials = "00000000000000000000000000000000000000";
            
            var identity = _identityStorage.GetByName(username);
            var saltedHash = Encryptor.GenerateSaltedHash(credentialsBytes, identity.TokenSalt);
            // fast credentials clear to avoid any kind of memory sniff
            Array.Clear(credentialsBytes, 0, credentialsBytes.Length);

            if (!Arrays.CompareByteArrays(identity.TokenHash, saltedHash))
            {
                return null;
            }

            // create session
            var now = DateTime.Now;
            const int sessionTimeoutMinutes = 30;
            var expireAt = now.AddMinutes(sessionTimeoutMinutes);
            var sessionHash = Guid.NewGuid().ToString();
            var session = new Models.Session(sessionHash, identity.Username, now, expireAt);
            _sessionManager.Create(session);
            
            return session;
        }
    }
}