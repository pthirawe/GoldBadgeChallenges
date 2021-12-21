using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_KomodoGreenPlan.Repository
{
    public class CarRepository
    {
        private List<Vehicle> _gasVehicles = new List<Vehicle>();
        private List<Vehicle> _hybridVehicles = new List<Vehicle>();
        private List<Vehicle> _electricVehicles = new List<Vehicle>();
        private int _count = 0;

        //Create
        public bool AddNewVehicle(Vehicle newVehicle)
        {
            if(newVehicle == null)
            {
                return false;
            }
            newVehicle.ID = _count++;
            switch (newVehicle.FuelType)
            {
                case FuelType.Gas:
                    _gasVehicles.Add(newVehicle);
                    return true;
                case FuelType.Hybrid:
                    _hybridVehicles.Add(newVehicle);
                    return true;
                case FuelType.Electric:
                    _electricVehicles.Add(newVehicle);
                    return true;
                default:
                    Console.WriteLine("Unable to add new vehicle. Invalid Fuel Type.");
                    return false;
            }
        }
        //Read
        public List<Vehicle> GetVehiclesByFuelType(FuelType fuelSelect)
        {
            switch (fuelSelect)
            {
                case FuelType.Gas:
                    return _gasVehicles;
                case FuelType.Hybrid:
                    return _hybridVehicles;
                case FuelType.Electric:
                    return _electricVehicles;
                default:
                    return null;
            }
        }
        public Vehicle GetVehicleByID(int searchID)
        {
            foreach(Vehicle vehicle in _gasVehicles)
            {
                if(vehicle.ID == searchID)
                {
                    return vehicle;
                }
            }
            foreach(Vehicle vehicle in _hybridVehicles)
            {
                if(vehicle.ID == searchID)
                {
                    return vehicle;
                }
            }
            foreach(Vehicle vehicle in _electricVehicles)
            {
                if(vehicle.ID == searchID)
                {
                    return vehicle;
                }
            }
            return null;
        }
        //Update
        public bool UpdateVehicle(int searchID, Vehicle updatedVehicle)
        {
            Vehicle outdatedVehicle = GetVehicleByID(searchID);
            if(outdatedVehicle == null)
            {
                return false;
            }
            if (outdatedVehicle.FuelType == updatedVehicle.FuelType)
            {
                outdatedVehicle.Make = updatedVehicle.Make;
                outdatedVehicle.Model = updatedVehicle.Model;
                //outdatedVehicle.FuelEconomy = updatedVehicle.FuelEconomy;
                //outdatedVehicle.MSRP = updatedVehicle.MSRP;
                return true;
            }
            else
            {
                updatedVehicle.ID = outdatedVehicle.ID;
                switch (outdatedVehicle.FuelType)
                {
                    case FuelType.Gas:
                        _gasVehicles.Remove(outdatedVehicle);
                        break;
                    case FuelType.Hybrid:
                        _hybridVehicles.Remove(outdatedVehicle);
                        break;
                    case FuelType.Electric:
                        _electricVehicles.Remove(outdatedVehicle);
                        break;
                    default:
                        return false;
                }
                switch (updatedVehicle.FuelType)
                {
                    case FuelType.Gas:
                        _gasVehicles.Add(updatedVehicle); 
                        break;
                    case FuelType.Hybrid:
                        _hybridVehicles.Add(updatedVehicle);
                        break;
                    case FuelType.Electric:
                        _electricVehicles.Add(updatedVehicle);
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }
        //Delete
        public bool RemoveVehicle(Vehicle vehicleToRemove)
        {
            switch (vehicleToRemove.FuelType)
            {
                case FuelType.Gas:
                    return _gasVehicles.Remove(vehicleToRemove);
                case FuelType.Hybrid:
                    return _hybridVehicles.Remove(vehicleToRemove);
                case FuelType.Electric:
                    return _electricVehicles.Remove(vehicleToRemove);
                default:
                    return false;
            }
        }
    }
}
