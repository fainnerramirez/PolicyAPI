using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NUnit.Framework;
using Policies.Controllers;
using Policies.Models;

namespace Policies.Tests
{
    public class PolicyTest
    {
        private PolicieController _controller;

        [SetUp]
        public void Setup()
        {
            var databaseName = "test_policy";
            var client = new MongoClient();
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<Policie>("policies");

            //_controller = new InsurancePolicyController(new InsurancePolicyContext(databaseName, collection));
            //_controller = new PoliciesSettings(new PoliciesSettings(databaseName, collection));
        }

        [Test]
        public void GetPolicyByNumber_ExistingPolicy_ReturnsPolicy()
        {
            // Arrange
            var testPolicy = new Policie
            {
                NumeroPoliza = 123456,
                NombreCliente = "John Doe",
                CiudadResidenciaCliente = "Bogotá",
                CoberturasPolizas = new List<string> { "accidentes, incendios, robo" },
                DireccionResidenciaCliente = "calle 13",
                FechaFinVigencia = DateTime.Now,
                FechaInicioVigencia = DateTime.Now,
                FechaNacimientoCliente = DateTime.Now,
                FechaRegistroPoliza = DateTime.Now,
                IdentificacionCliente = "cliente123",
                ModeloAutoMotor = "MOD123",
                NombrePlanPoliza = "AZME",
                PlacaAutoMotor = "ACE456",
                ValorMaximoPoliza = 100000,
                VehiculoInspeccion = false
            };

            //InsertTestPolicy(testPolicy);

            var result = _controller.GetPolicieNumber(123456);

            Assert.IsInstanceOf<ActionResult<Policie>>(result);
            Assert.AreEqual(testPolicy, result.Value);
        }

        [Test]
        public void GetPolicyByNumber_NonexistentPolicy_ReturnsNotFound()
        {
            // Act
            var result = _controller.GetPolicieNumber(123456);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        // Agrega más pruebas unitarias para los demás métodos del controlador

        //private void InsertTestPolicy(Policie policy)
        //{
        //    var context = GetTestContext();
        //    context.InsurancePolicies.InsertOne(policy);
        //}

        //private InsurancePolicyContext GetTestContext()
        //{
        //    var settings = CreateTestDatabaseSettings();
        //    var client = new MongoClient(settings.ConnectionString);
        //    var database = client.GetDatabase(settings.DatabaseName);
        //    return new InsurancePolicyContext(database);
        //}

        //private DatabaseSettings CreateTestDatabaseSettings()
        //{
        //    return new DatabaseSettings
        //    {
        //        ConnectionString = "mongodb://localhost:27017",
        //        DatabaseName = "TestDatabase"
        //    };
        //}
    }
}
