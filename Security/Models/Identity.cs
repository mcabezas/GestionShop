namespace Security.Models
{
    public class Identity
    {
        public Identity(string username)
        {
            Username = username;
        }

        public Identity(string username, Permission permission)
        {
            Username = username;
            Permission = permission;
        }

        public Identity(string username, byte[] tokenHash, byte[] tokenSalt)
        {
            Username = username;
            TokenHash = tokenHash;
            TokenSalt = tokenSalt;
        }

        public Identity(string username, byte[] tokenHash, byte[] tokenSalt, Permission permission)
        {
            Username = username;
            TokenHash = tokenHash;
            TokenSalt = tokenSalt;
            Permission = permission;
        }

        public string Username { get; }
        public byte[] TokenHash { get; }
        public byte[] TokenSalt { get; }

        public Permission Permission { get; }

    }
}