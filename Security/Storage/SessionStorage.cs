using System;
using System.Collections.Generic;
using Security.Models;

namespace Security.Storage
{
    public class SessionStorage : ISessionStorage
    {
        private readonly Dictionary<string, Session> _sessions = new();

        public void Create(Session session)
        {
            _sessions.Add(session.Hash, session);
        }

        public Session Get(string hash)
        {
            if (!_sessions.ContainsKey(hash))
            {
                return null;
            }

            var sess = _sessions[hash];

            if (DateTime.Now.CompareTo(sess.ExpireAt) > 0)
            {
                // If the session has expired then remove it
                _sessions.Remove(hash);
                return null;
            }

            return sess;
        }
    }
}