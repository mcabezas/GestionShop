using System;
using System.Text;
using Libs.Exceptions;
using Security.Business.Authentication;
using Security.Models;
using Security.Storage;

namespace Security.Business.AccessManagement
{
    public interface IIdentityManager
    {
        public Identity CreateIdentity(string username, string credentials, Permission permission);
        public Identity Get(string username);
        public Identity UpdatePermission(Identity identity, Permission permission);
    }
}