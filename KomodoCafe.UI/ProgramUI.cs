using _01_KomodoCafe.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoCafe.UI
{
    public class ProgramUI
    {
        private readonly MenuItemRepository _menuRepo;
        public ProgramUI()
        {
            _menuRepo = new MenuItemRepository();
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
                Console.WriteLine("Komodo Cafe\n" +
                    "1. List menu items\n" +
                    "2. Add new menu item.\n" +
                    "3. Remove item from menu.\n" +
                    "0. Exit");

                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "0":
                        return;
                    case "1":
                        ListMenuItems();
                        break;
                    case "2":
                        AddNewItem();
                        break;
                    case "3":
                        RemoveMenuItem();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        break;
                }
            }
        }
        public void ListMenuItems()
        {
            foreach(MenuItem item in _menuRepo.GetMenu())
            {
                DisplayMenuItemDetails(item);
            }
            WaitForKey();
        }

        public void AddNewItem()
        {
            string itemName;
            string description;
            string ingredients;
            double price;

            Console.WriteLine("Please enter the name of the new item: ");
            itemName = Console.ReadLine();
            Console.WriteLine("Please enter a description for the menu item: ");
            description = Console.ReadLine();
            Console.WriteLine("Please enter a list of ingredients separated by a comma [,].");
            ingredients = Console.ReadLine();
            Console.WriteLine("Please enter a price for the new item.");
            while(!Double.TryParse(Console.ReadLine(),out price))
            {
                Console.WriteLine("Please enter a valid number.");
            }
            MenuItem newMenuItem = new MenuItem(itemName, description, BuildIngredientList(ingredients), price);
            if(!_menuRepo.AddMenuItem(newMenuItem))
            {
                Console.WriteLine("Problem adding item to menu.");
            }
            Console.WriteLine("Successfully added item to menu.");
        }

        public void RemoveMenuItem()
        {
            int toRemove;
            Console.WriteLine("Please enter the number of the menu item to remove.");
            while(!Int32.TryParse(Console.ReadLine(),out toRemove))
            {
                Console.WriteLine("Please enter a valid number.");
            }
            DisplayMenuItemDetails(_menuRepo.GetMenuItem(toRemove));
            Console.WriteLine("Remove this item?");
            if(!ConfirmSelection())
            {
                Console.WriteLine("Cancelled.");
                WaitForKey();
                return;
            }
            if(!_menuRepo.RemoveMenuItem(toRemove))
            {
                Console.WriteLine("Problem removing item.");
                WaitForKey();
                return;
            }
            Console.WriteLine("Successfully removed item");
        }

        // Helper Methods

        private void WaitForKey()
        {
            Console.WriteLine("Press Any Key to Continue.");
            Console.ReadKey();
        }
        private void DisplayMenuItemDetails(MenuItem item)
        {
            if(item == null)
            {
                Console.WriteLine("Item does not exist.");
                return;
            }
            Console.WriteLine(
                $"Meal Number: {string.Format("{0:00}", item.MealNumber)}\n" +
                $"Meal Name: {item.MealName}\n" +
                $"Description: {item.Description}\n" +
                $"Ingredients: " + (string.Join(",", item.Ingredients)) + "\n" +
                $"Price: ${string.Format("{0:0.00}", item.Price)}"
                );
            Console.WriteLine("----------------------------------");
        }
        private List<string> BuildIngredientList(string list)
        {
            List<string> ingredientList = new List<string>();
            ingredientList = list.Split(',').ToList();
            return ingredientList;
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

        //Testing Methods
        public void Seed()
        {
            string ingredients1 = "dough,tomatoes,pepperoni,cheese";
            string ingredients2 = "beef,potatoes,shrimp,garlic,pepper,salt,brusselsprouts";
            string ingredients3 = "lobster,cream,cheese,tomatoes,garlic,parsley";
            MenuItem meal1 = new MenuItem("Pizza", "Baked to perfection", BuildIngredientList(ingredients1), 5.49);
            MenuItem meal2 = new MenuItem("Steak", "Seared to order", BuildIngredientList(ingredients2), 12.99);
            MenuItem meal3 = new MenuItem("Lobster Thermidore", "Succulent and sweet.", BuildIngredientList(ingredients3), 21.00);

            _menuRepo.AddMenuItem(meal1);
            _menuRepo.AddMenuItem(meal2);
            _menuRepo.AddMenuItem(meal3);

        }
    }

}
