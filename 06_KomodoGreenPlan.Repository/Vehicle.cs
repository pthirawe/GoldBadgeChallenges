using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_KomodoGreenPlan.Repository
{
    public enum FuelType { Gas, Hybrid, Electric }
    //public enum Type { Sedan, Hatchback, SUV, Pickup, Van }
    public class Vehicle
    {
        public Vehicle()
        {

        }
        public Vehicle(string make, string model, FuelType fuelType)
        {
            Make = make;
            Model = model;
            FuelType = fuelType;
        }
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public FuelType FuelType { get; set; }

        // Add Other properties as necessary
        /*
        public double 
        public double FuelEconomy { get; set; }
        public double MSRP { get; set; }
        */

    }
}
