using System;
using System.Text;
using Libs.Exceptions;
using Security.Business.Authentication;
using Security.Models;
using Security.Storage;

namespace Security.Business.AccessManagement
{
    public class IdentityManager : IIdentityManager
    {
        private readonly IIdentityStorage _identityStorage;

        public IdentityManager(IIdentityStorage identityStorage)
        {
            _identityStorage = identityStorage;
        }

        public Identity CreateIdentity(string username, string credentials, Permission permission = null)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new InvalidArgumentException(
                    $"Username cannot be empty");
            }

            if (string.IsNullOrEmpty(credentials))
            {
                throw new InvalidArgumentException(
                    $"Credentials cannot be empty");
            }

            var credentialsBytes = Encoding.UTF8.GetBytes(credentials);
            var salt = Encryptor.CreateSalt();
            var hash = Encryptor.GenerateSaltedHash(credentialsBytes, salt);

            // fast credentials clear to avoid any kind of memory sniff
            Array.Clear(credentialsBytes, 0, credentialsBytes.Length);

            var identity = new Identity(username, hash, salt, permission);
            _identityStorage.Save(identity);
            return new Identity(identity.Username);
        }


        public Identity Get(string username)
        {
            var identity = _identityStorage.GetByName(username);
            // password details must not be public
            return new Identity(identity.Username, identity.Permission);
        }

        public Identity UpdatePermission(Identity identity, Permission permission)
        {
            var updated = new Identity(identity.Username, permission);
            _identityStorage.UpdatePermission(updated);
            return updated;
        }
    }
}