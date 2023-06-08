using Policies.Models;
using MongoDB.Driver;
using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Mvc;

namespace Policies.Services
{
    public class PolicieService : IPoliciesServices
    {
        private readonly IMongoCollection<Policie> _policies;
        public PolicieService( PoliciesSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _policies = database.GetCollection<Policie>(settings.PoliciesCollectionName);
        }

        public Policie Create(Policie policie)
        {
            _policies.InsertOne(policie);

            return policie;
        }

        public List<Policie> GetAll()
        {
            return _policies.Find(policie => true).ToList();
        }

        public Policie Get(string id)
        {
            return _policies.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Update(string id, Policie policie)
        {
            _policies.ReplaceOne(policie => policie.Id == id, policie);
        }
        public void Delete(string id)
        {
            _policies.DeleteOne(policie => policie.Id == id);
        }

        public Policie GetPoliciePlate(string plate)
        {
            return _policies.Find(policie => policie.PlacaAutoMotor == plate).FirstOrDefault();
        }

        public Policie GetNumberPolicie(int numberpolicie)
        {
            return _policies.Find(policie => policie.NumeroPoliza == numberpolicie).FirstOrDefault();
        }
    }
}
