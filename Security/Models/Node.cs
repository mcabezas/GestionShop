using System.Collections.Generic;
using System.Collections.Immutable;

namespace Security.Models
{
    public class Node : Permission
    {
        private readonly ImmutableList<Permission> _permissions;

        public Node(string name, string description, ImmutableList<Permission> permissions, bool banned) : base(name,
            description, banned)
        {
            _permissions = permissions;
        }

        public Node(string name, string description, bool banned) : base(name, description, banned)
        {
            _permissions = ImmutableList<Permission>.Empty;
        }

        public override ImmutableList<Permission> GetLeaves()
        {
            var permissions = new List<Permission>();
            foreach (var p in _permissions)
            {
                permissions.AddRange(p.GetLeaves());
            }

            return ImmutableList.Create(permissions.ToArray());
        }

        public override ImmutableList<Permission> GetChildren()
        {
            return _permissions;
        }

        public override Permission Ban()
        {
            return new Node(Name, Description, _permissions, true);
        }

        public override bool Contains(Permission permission)
        {
            foreach (var p in _permissions)
            {
                if (p.Name == permission.Name)
                {
                    return true;
                }

                if (p.Contains(permission))
                {
                    return true;
                }
            }

            return false;
        }
    }
}