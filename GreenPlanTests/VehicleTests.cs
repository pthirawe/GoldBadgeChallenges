using _06_KomodoGreenPlan.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenPlanTests
{
    [TestClass]
    public class VehicleTests
    {
        [TestMethod]
        public void CreateVehicle_ShouldSetCorrectValues()
        {
            Vehicle vehicle = new Vehicle();
            vehicle.Make = "Tesla";
            vehicle.Model = "Model X";

            string expectedMake = "Tesla";
            string actualMake = vehicle.Make;
            string expectedModel = "Model X";
            string actualModel = vehicle.Model;

            Assert.AreEqual(expectedMake, actualMake);
        }

        [TestMethod]
        public void TestConstructor_ShouldSetCorrectValues()
        {
            Vehicle vehicle = new Vehicle("Make", "Model", FuelType.Gas);

            string expectedMake = "Make";
            string actualMake = vehicle.Make;
            string expectedModel = "Model";
            string actualModel = vehicle.Model;
            FuelType expectedFuelType = FuelType.Gas;
            FuelType actualFuelType = vehicle.FuelType;

            Assert.AreEqual(expectedMake, actualMake);
            Assert.AreEqual(expectedModel, actualModel);
            Assert.AreEqual(expectedFuelType, actualFuelType);
        }

    }
}
