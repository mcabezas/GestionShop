using System;
using System.Collections.Immutable;

namespace Security.Models
{
    public class Session
    {
        public readonly string Hash;
        public readonly string Username;
        public DateTime LoginAt;
        public DateTime ExpireAt;

        public Session(string hash, string username, DateTime loginAt, DateTime expireAt)
        {
            Hash = hash;
            Username = username;
            LoginAt = loginAt;
            ExpireAt = expireAt;
        }
    }
}