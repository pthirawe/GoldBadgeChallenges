using _06_KomodoGreenPlan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlan.UI
{
    public class GreenPlanUI
    {
        private readonly CarRepository _carRepo;

        public GreenPlanUI()
        {
            _carRepo = new CarRepository();
        }

        public void Run()
        {
            Seed();
            DisplayMenu();
        }

        public void DisplayMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(
                    "Welcome to Komodo Insurance Green Plan Database\n" +
                    "1. List Vehicles.\n" +
                    "2. Add new vehicle.\n" +
                    "3. Update existing vehicle.\n" +
                    "4. Delete vehicle.\n" +
                    "0. Exit"
                    );
                string userInput = Console.ReadLine();
                Console.Clear();
                switch(userInput)
                {
                    case "0":
                        return;
                    case "1":
                        ListVehicles();
                        break;
                    case "2":
                        AddNewVehicle();
                        break;
                    case "3":
                        UpdateExistingVehicle();
                        break;
                    case "4":
                        DeleteVehicle();
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection.");
                        break;

                }
            }
        }

        public void ListVehicles()
        {
            string userInput;
            List<Vehicle> vehicleList = new List<Vehicle>();
            bool isValid = false;

            Console.WriteLine("Please select from available options.\n" +
                "1. List Gas Vehicles\n" +
                "2. List Hybrid Vehicles\n" +
                "3. List Electric Vehicles\n" +
                "4. List All Vehicles.\n" +
                "0. Cancel.");

            do
            {
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "0":
                        Console.WriteLine("Cancelled.");
                        return;
                    case "1":
                    case "2":
                    case "3":
                        vehicleList = _carRepo.GetVehiclesByFuelType((FuelType)Int32.Parse(userInput)-1);
                        isValid = true;
                        break;
                    case "4":
                        vehicleList = new List<Vehicle>();
                        for (int i = 0; i < 3; i++)
                        {
                            vehicleList.AddRange(_carRepo.GetVehiclesByFuelType((FuelType)i));
                        }
                        isValid = true;
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection.");
                        break;
                }
            } while (!isValid);
            foreach (Vehicle vehicle in vehicleList)
            {
                DisplayVehicleDetails(vehicle);
            }
            WaitForKey();
        }

        public void AddNewVehicle()
        {

            if(!_carRepo.AddNewVehicle(BuildNewVehicle()))
            {
                Console.WriteLine("Problem adding vehicle.  Returning to main menu.");
            }
            else
            {
                Console.WriteLine("Vehicle added successfully.");
            }
        }

        public void UpdateExistingVehicle()
        {
            int searchID;
            Vehicle vehicleToUpdate;
            Vehicle newVehicle;

            Console.WriteLine("Please enter the vehicle ID to update.");
            if(!Int32.TryParse(Console.ReadLine(), out searchID))
            {
                Console.WriteLine("Invalid Vehicle ID.  Returning to main menu.");
                WaitForKey();
                return;
            }
            vehicleToUpdate = _carRepo.GetVehicleByID(searchID);
            if (vehicleToUpdate == null)
            {
                Console.WriteLine("Invalid Vehicle ID.  Returning to main menu.");
                WaitForKey();
                return;
            }
            DisplayVehicleDetails(vehicleToUpdate);
            Console.WriteLine("Update this vehicle?");
            if(!ConfirmSelection())
            {
                Console.WriteLine("Cancelled.  Returning to main menu.");
                WaitForKey();
                return;
            }
            newVehicle = BuildNewVehicle();
            if(!_carRepo.UpdateVehicle(searchID, newVehicle))
            {
                Console.WriteLine("Problem updating vehicle. Returning to main menu.");
                WaitForKey();
                return;
            }
        }

        public void DeleteVehicle()
        {
            Vehicle vehicleToDelete = SearchForVehicle();
            DisplayVehicleDetails(vehicleToDelete);
            Console.WriteLine("Delete this vehicle?");
            if (!ConfirmSelection())
            {
                Console.WriteLine("Cancelled.  Returning to main menu.");
                WaitForKey();
                return;
            }
            if(!_carRepo.RemoveVehicle(vehicleToDelete))
            {
                Console.WriteLine("Problem deleting vehicle. Returning to main menu.");
                WaitForKey();
                return;
            }

        }

        // Helper Methods
        private void WaitForKey()
        {
            Console.WriteLine("Press Any Key to Continue.");
            Console.ReadKey();
        }
        private void DisplayVehicleDetails(Vehicle thisVehicle)
        {
            if (thisVehicle == null)
            {
                Console.WriteLine("Vehicle does not exist.");
                return;
            }
            Console.WriteLine(
                $"ID: {string.Format("{0:00000}", thisVehicle.ID)} \n" +
                $"Make: {thisVehicle.Make}\n" +
                $"Model: {thisVehicle.Model}\n" +
                $"Fuel Type: " + FuelEnumToText(thisVehicle.FuelType)
                );
            Console.WriteLine("----------------------------------");
        }
        private Vehicle BuildNewVehicle()
        {
            string make;
            string model;
            int fuelInput;
            FuelType fuelType;

            Console.WriteLine("Please Enter Vehicle Make: ");
            make = Console.ReadLine();
            Console.WriteLine("Please Enter Vehicle Model: ");
            model = Console.ReadLine();
            Console.WriteLine(
                "Please Select a fuel type:\n" +
                "1. Gas\n" +
                "2. Hybrid\n" +
                "3. Electric"
                );
            while (!Int32.TryParse(Console.ReadLine(), out fuelInput))
            {
                Console.WriteLine("Please enter a valid selection.");
            }

            fuelType = (FuelType)(fuelInput-1);

            return new Vehicle(make, model, fuelType);
        }
        private string FuelEnumToText(FuelType fuelType)
        {
            switch (fuelType)
            {
                case FuelType.Gas:
                    return "Gas";
                case FuelType.Hybrid:
                    return "Hybrid";
                case FuelType.Electric:
                    return "Electric";
                default:
                    return "Invalid Fuel Type";
            }
        }
        private bool ConfirmSelection()
        {
            bool isValid;
            do
            {
                string confirm = Console.ReadLine();
                isValid = (confirm.ToLower() == "y") || (confirm.ToLower() == "n");

                switch (confirm.ToLower())
                {
                    case "y":
                        return true;
                    //break;
                    case "n":
                        return false;
                    //break;
                    default:
                        Console.WriteLine("Please use y or n to respond.");
                        break;
                }

            } while (!isValid);
            return false;
        }
        private Vehicle SearchForVehicle()
        {
            int searchID;

            Console.WriteLine("Please enter the vehicle ID to update.");
            if (!Int32.TryParse(Console.ReadLine(), out searchID))
            {
                Console.WriteLine("Invalid Vehicle ID.");
                WaitForKey();
                return null;
            }
            return _carRepo.GetVehicleByID(searchID);
        }

        // Debug methods
        public void Seed()
        {
            Vehicle test1 = new Vehicle("Honda", "Civic", FuelType.Gas);
            Vehicle test2 = new Vehicle("Toyota", "Camry", FuelType.Gas);
            Vehicle test3 = new Vehicle("Ford", "Fiesta", FuelType.Gas);
            Vehicle test4 = new Vehicle("Chevrolet", "Volt", FuelType.Hybrid);
            Vehicle test5 = new Vehicle("Honda", "Civic", FuelType.Hybrid);
            Vehicle test6 = new Vehicle("Toyota", "Prius", FuelType.Hybrid);
            Vehicle test7 = new Vehicle("Tesla", "Model X", FuelType.Electric);
            Vehicle test8 = new Vehicle("Tesla", "Model 3", FuelType.Electric);
            Vehicle test9 = new Vehicle("BMW", "i3", FuelType.Electric);

            _carRepo.AddNewVehicle(test1);
            _carRepo.AddNewVehicle(test2);
            _carRepo.AddNewVehicle(test3);
            _carRepo.AddNewVehicle(test4);
            _carRepo.AddNewVehicle(test5);
            _carRepo.AddNewVehicle(test6);
            _carRepo.AddNewVehicle(test7);
            _carRepo.AddNewVehicle(test8);
            _carRepo.AddNewVehicle(test9);
        }
    }
}
