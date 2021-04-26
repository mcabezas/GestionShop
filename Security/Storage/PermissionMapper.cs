using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Security.Models;

namespace Security.Storage
{
    public static class PermissionMapper
    {
        private enum PermissionType
        {
            Leaf,
            Node
        }

        public static Permission ToEntity(DataRow dr, bool banned = false)
        {
            var permissionType = Enum.Parse<PermissionType>((string) dr[Database.PermissionColumnNameType]);
            if (permissionType == PermissionType.Leaf)
            {
                return new Leaf(
                    (string) dr[Database.PermissionColumnNameName],
                    (string) dr[Database.PermissionColumnNameDescription],
                    banned
                );
            }

            var childRows = dr.GetChildRows(Database.RelationOneToManyPermissionPermission);
            var children = new Collection<Permission>();
            foreach (var child in childRows)
            {
                var c = child.GetParentRow(Database.RelationOneToManyPermissionPermissionChild);
                children.Add(ToEntity(c, (bool)child[Database.PermissionPermissionColumnNameBanned]));
            }
            
            return new Node((string) dr[Database.PermissionColumnNameName],
                (string) dr[Database.PermissionColumnNameDescription],
                ImmutableList.Create(children.ToArray()), banned);
        }

        public static DataRow ToRow(Permission permission, DataTable dt)
        {
            var dr = dt.NewRow();
            dr[Database.PermissionColumnNameName] = permission.Name;
            dr[Database.PermissionColumnNameDescription] = permission.Description;
            dr[Database.PermissionColumnNameType] =
                permission is Leaf ? PermissionType.Leaf : PermissionType.Node;
            return dr;
        }

        public static Dictionary<string, List<DataRow>> ToChildRows(Permission permission, DataTable dt)
        {
            return ToChildRows(permission, permission.GetChildren(), dt);
        }

        private static Dictionary<string, List<DataRow>> ToChildRows(Permission parent,
            ImmutableList<Permission> children,
            DataTable dt)
        {
            var childRows = new Dictionary<string, List<DataRow>>();
            foreach (var child in children)
            {
                var dataRow = dt.NewRow();
                dataRow[Database.PermissionPermissionColumnNameParent] = parent.Name;
                dataRow[Database.PermissionPermissionColumnNameChild] = child.Name;
                dataRow[Database.PermissionPermissionColumnNameBanned] = child.Banned;

                if (!childRows.ContainsKey(parent.Name))
                {
                    childRows.Add(parent.Name, new List<DataRow>());
                }

                childRows[parent.Name].Add(dataRow);
            }

            return childRows;
        }

        public static void AddPermissionPermissionRow(Permission parent, Permission child, DataTable dt)
        {
            dt.Rows.Add(parent.Name, child.Name, child.Banned);
        }
    }
}