namespace Policies.Models
{
    public class UserSettings : IUserSettings
    {
        public string UsersCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
