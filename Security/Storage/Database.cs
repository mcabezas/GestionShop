using System.Data;

namespace Security.Storage
{
    public class Database
    {
        public const string PermissionTableName = "permission";
        public const string PermissionColumnNameName = "name";
        public const string PermissionColumnNameDescription = "description";
        public const string PermissionColumnNameType = "type";

        public const string PermissionPermissionTableName = "permission_permission";
        public const string PermissionPermissionColumnNameParent = "parent_name";
        public const string PermissionPermissionColumnNameChild = "child_name";
        public const string PermissionPermissionColumnNameBanned = "banned";

        public const string IdentityTableName = "identity";
        public const string IdentityColumnNameUsername = "username";
        public const string IdentityColumnNameHash = "token_hash";
        public const string IdentityColumnNameSalt = "token_salt";
        public const string IdentityColumnNamePermission = "permission_name";

        public const string RelationOneToManyPermissionIdentity = "one_to_many__permission_identities";
        public const string RelationOneToManyPermissionPermission = "one_to_many__permission_permission";
        public const string RelationOneToManyPermissionPermissionChild = "one_to_many__permission_permission_child";

        public Database(string fileNameXml = "database.xml")
        {
            _fileNameXmlXml = fileNameXml;

            #region table_permission

            // Table permission
            var permissionsTable = new DataTable(PermissionTableName);
            var permissionsNameColumn = new DataColumn(PermissionColumnNameName)
                {DataType = typeof(string)};
            var permissionDescriptionColumn = new DataColumn(PermissionColumnNameDescription)
                {DataType = typeof(string)};
            var permissionTypeColumn = new DataColumn(PermissionColumnNameType)
                {DataType = typeof(string)};
            permissionsTable.Columns.AddRange(new[]
            {
                permissionsNameColumn,
                permissionDescriptionColumn,
                permissionTypeColumn
            });
            permissionsTable.PrimaryKey = new[] {permissionsNameColumn};
            _dataset.Tables.Add(permissionsTable);

            #endregion

            #region table_permission_permission

            // Table permission_permission
            var permissionsPermissionsTable = new DataTable(PermissionPermissionTableName);
            var permPermParentColumn = new DataColumn(PermissionPermissionColumnNameParent)
                {DataType = typeof(string)};
            var permPermChildColumn = new DataColumn(PermissionPermissionColumnNameChild)
                {DataType = typeof(string)};
            var permPermBannedColumn = new DataColumn(PermissionPermissionColumnNameBanned)
                {DataType = typeof(bool)};
            permissionsPermissionsTable.Columns.AddRange(new[]
                {permPermParentColumn, permPermChildColumn, permPermBannedColumn});
            permissionsPermissionsTable.PrimaryKey = new[] {permPermParentColumn, permPermChildColumn};
            _dataset.Tables.Add(permissionsPermissionsTable);

            #endregion

            #region table_identity

            // Table identity
            var identityTable = new DataTable(IdentityTableName);
            var identityUsernameColumn = new DataColumn(IdentityColumnNameUsername)
                {DataType = typeof(string)};
            var identityHashColumn = new DataColumn(IdentityColumnNameHash) {DataType = typeof(byte[])};
            var identitySaltColumn = new DataColumn(IdentityColumnNameSalt) {DataType = typeof(byte[])};
            var identityPermissionColumn = new DataColumn(IdentityColumnNamePermission)
                {DataType = typeof(string)};
            identityTable.Columns.AddRange(new[]
            {
                identityUsernameColumn,
                identityHashColumn,
                identitySaltColumn,
                identityPermissionColumn
            });
            identityTable.PrimaryKey = new[] {identityUsernameColumn};
            _dataset.Tables.Add(identityTable);
            //tRR.Constraints.Add(new ForeignKeyConstraint("role_fk", tR_RoleNameColumn, tRR_RoleColumn));

            _dataset.Relations.Add(new DataRelation(RelationOneToManyPermissionIdentity, permissionsNameColumn,
                identityPermissionColumn));
            _dataset.Relations.Add(new DataRelation(RelationOneToManyPermissionPermission, permissionsNameColumn,
                permPermParentColumn));
            _dataset.Relations.Add(new DataRelation(RelationOneToManyPermissionPermissionChild, permissionsNameColumn,
                permPermChildColumn));

            #endregion
        }

        private readonly DataSet _dataset = new DataSet();
        private string _fileNameXmlXml;

        public DataSet DataSet()
        {
            return _dataset;
        }

        public void WriteXml()
        {
            _dataset.WriteXml(_fileNameXmlXml);
        }

        public DataSet ReadXml()
        {
            _dataset.ReadXml(_fileNameXmlXml);
            return _dataset;
        }

        public DataTable GetTable(string tableName)
        {
            return _dataset.Tables[tableName];
        }
    }
}