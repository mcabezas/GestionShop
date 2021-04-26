using Libs.Exceptions;
using Security.Models;

namespace Security.Storage
{
    public class IdentityStorage : IIdentityStorage
    {
        private readonly Database _database;

        public IdentityStorage(Database database)
        {
            _database = database;
        }

        public void Save(Identity identity)
        {
            var dt = _database.GetTable(Database.IdentityTableName);
            dt.Rows.Add(IdentityMapper.ToRow(identity, dt));
            _database.WriteXml();
        }

        public Identity GetByName(string username)
        {
            var dt = _database.GetTable(Database.IdentityTableName);
            var row = dt.Rows.Find(username);
            return IdentityMapper.ToIdentity(row, dt);
        }

        public void UpdatePermission(Identity identity)
        {
            var dt = _database.GetTable(Database.IdentityTableName);
            var row = dt.Rows.Find(identity.Username);
            if (row == null)
            {
                throw new EntityNotFoundException($@"Identity [{identity.Username}] not found");
            }
            
            IdentityMapper.UpdatePermission(identity, row);
        }
    }
}