using System;
using System.Collections.Immutable;

namespace Security.Models
{
    public class Leaf : Permission
    {
        public Leaf(string name, string description, bool banned = false) : base(name, description, banned)
        {
        }

        public override ImmutableList<Permission> GetLeaves()
        {
            return ImmutableList.Create<Permission>(this);
        }

        public override ImmutableList<Permission> GetChildren()
        {
            return ImmutableList<Permission>.Empty;
        }

        public override Permission Ban()
        {
            return new Leaf(Name, Description, true);
        }

        public override bool Contains(Permission permission)
        {
            return permission.Name == Name;
        }
    }
}