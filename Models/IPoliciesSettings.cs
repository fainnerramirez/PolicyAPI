
namespace Policies.Models
{
    public interface IPoliciesSettings
    {
        string PoliciesCollectionName { get; set; } 
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}