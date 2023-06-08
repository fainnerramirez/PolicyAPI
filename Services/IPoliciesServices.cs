using Policies.Models;

namespace Policies.Services
{
    public interface IPoliciesServices
    {
        List<Policie> GetAll();
        Policie Get(string id);
        Policie GetPoliciePlate(string plate);
        Policie GetNumberPolicie(int numbrepolicie);
        Policie Create(Policie policie);
        void Delete(string id);
        void Update(string id, Policie policie);

    }
}
