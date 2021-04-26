using System.Collections.Immutable;

namespace Security.Models
{
    public abstract class Permission
    {
        public string Name { get; }
        public string Description { get; }

        public bool Banned { get; }

        protected Permission(string name, string description, bool banned)
        {
            Name = name;
            Description = description;
            Banned = banned;
        }

        public abstract ImmutableList<Permission> GetLeaves();
        public abstract ImmutableList<Permission> GetChildren();

        public abstract Permission Ban();

        public abstract bool Contains(Permission permission);
    }
}