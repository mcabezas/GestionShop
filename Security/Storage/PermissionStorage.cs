using System.Collections.Generic;
using System.Data;
using Security.Models;

namespace Security.Storage
{
    public class PermissionStorage : IPermissionStorage
    {
        private readonly Database _database;

        public PermissionStorage(Database database)
        {
            _database = database;
        }

        public void Save(Permission permission)
        {
            var table = _database.GetTable(Database.PermissionTableName);
            var dr = PermissionMapper.ToRow(permission, table);
            table.Rows.Add(dr);

            // children
            var childrenTable = _database.GetTable(Database.PermissionPermissionTableName);
            var childRows = PermissionMapper.ToChildRows(permission, childrenTable);
            foreach (var child in childRows)
            {
                RemoveOldChildren(childrenTable, child);
                foreach (var dataRow in child.Value)
                {
                    childrenTable.Rows.Add(dataRow);
                }
            }

            _database.WriteXml();
        }

        private static void RemoveOldChildren(DataTable childrenTable, KeyValuePair<string, List<DataRow>> child)
        {
            var dataRows = childrenTable.Select("");
            foreach (var dataRow in dataRows)
            {
                if ((string) dataRow[Database.PermissionPermissionColumnNameParent] == child.Key)
                {
                    dataRow.Delete();
                }
            }
        }

        public Permission GetByName(string name)
        {
            var dt = _database.GetTable(Database.PermissionTableName);
            var dr = dt.Rows.Find(name);
            return dr == null ? null : PermissionMapper.ToEntity(dr);
        }

        public Permission GetByExample(Permission permission)
        {
            return permission == null ? null : GetByName(permission.Name);
        }

        public void AddInnerPermission(Permission parent, Permission child)
        {
            var table = _database.GetTable(Database.PermissionPermissionTableName);
            PermissionMapper.AddPermissionPermissionRow(parent, child, table);
            _database.WriteXml();
        }
    }
}