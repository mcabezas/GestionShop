using System.Data;
using Security.Models;

namespace Security.Storage
{
    public class IdentityMapper
    {
        public static DataRow ToRow(Identity identity, DataTable dt)
        {
            var dr = dt.NewRow();
            dr[Database.IdentityColumnNameUsername] = identity.Username;
            dr[Database.IdentityColumnNameHash] = identity.TokenHash;
            dr[Database.IdentityColumnNameSalt] = identity.TokenSalt;
            if (identity.Permission != null)
            {
                dr[Database.IdentityColumnNamePermission] = identity.Permission.Name;
            }

            return dr;
        }

        public static Identity ToIdentity(DataRow dr, DataTable dt)
        {
            Permission permission = null;
            if (dr[Database.IdentityColumnNamePermission] != System.DBNull.Value)
            {
                var permDataRow = dr.GetParentRow(Database.RelationOneToManyPermissionIdentity);
                permission = PermissionMapper.ToEntity(permDataRow);
            }
            return new Identity(
                (string) dr[Database.IdentityColumnNameUsername],
                (byte[]) dr[Database.IdentityColumnNameHash],
                (byte[]) dr[Database.IdentityColumnNameSalt],
                permission);
        }

        public static void UpdatePermission(Identity identity, DataRow dr)
        {
            dr[Database.IdentityColumnNamePermission] = identity.Permission.Name;
        }
    }
}