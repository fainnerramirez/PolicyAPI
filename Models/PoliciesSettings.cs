namespace Policies.Models
{
    public class PoliciesSettings : IPoliciesSettings
    {
        public string PoliciesCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}