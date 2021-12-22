using _06_KomodoGreenPlan.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GreenPlanTests
{
    [TestClass]
    public class CarRepositoryTest
    {
        [TestMethod]
        public void AddToRepository_ShouldReturnTrue()
        {
            Vehicle vehicle = new Vehicle("Make", "Model", FuelType.Gas);
            CarRepository repo = new CarRepository();

            bool addResut = repo.AddNewVehicle(vehicle);

            Assert.IsTrue(addResut);
        }

        CarRepository _repo; 
        [TestInitialize]
        public void Arrange()
        {
            _repo = new CarRepository();

            Vehicle vehicle0 = new Vehicle("Make", "Model", FuelType.Gas);
            Vehicle vehicle1 = new Vehicle("Dingo", "Dango", FuelType.Hybrid);
            Vehicle vehicle2 = new Vehicle("Ringo", "Rango", FuelType.Gas);
            Vehicle vehicle3 = new Vehicle("Jamba", "Juice", FuelType.Hybrid);
            Vehicle vehicle4 = new Vehicle("Maru", "Sushi", FuelType.Electric);
            Vehicle vehicle5 = new Vehicle("Lola", "Montez", FuelType.Electric);

            _repo.AddNewVehicle(vehicle0);
            _repo.AddNewVehicle(vehicle1);
            _repo.AddNewVehicle(vehicle2);
            _repo.AddNewVehicle(vehicle3);
            _repo.AddNewVehicle(vehicle4);
            _repo.AddNewVehicle(vehicle5);
        }

        [TestMethod]
        public void GetVehiclesTest_ShouldReturnCorrectNumberOfEntries()
        {
            List<Vehicle> allVehicles = new List<Vehicle>();

            foreach(FuelType fuelType in Enum.GetValues(typeof(FuelType)))
            {
                allVehicles.AddRange(_repo.GetVehiclesByFuelType(fuelType));
            }

            int expected = 6;
            int actual = allVehicles.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetVehicleByID_ShouldReturnVehicelWithExpectedID()
        {
            int expected = 4;
            Vehicle searchVehicle = _repo.GetVehicleByID(expected);

            int actual = searchVehicle.ID;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateVehicleTest_ShouldReturnTrue()
        {
            Vehicle testVehicle = new Vehicle("Mars", "Bar", FuelType.Hybrid);

            bool testSuccess = _repo.UpdateVehicle(4,testVehicle);

            Assert.IsTrue(testSuccess);
        }

        [TestMethod]
        public void RemoveVehicleTest_ShouldReturnTrue()
        {
            Vehicle vehicleToRemove = _repo.GetVehicleByID(4);

            bool removalSuccess = _repo.RemoveVehicle(vehicleToRemove);

            Assert.IsTrue(removalSuccess);
        }
    }
}
