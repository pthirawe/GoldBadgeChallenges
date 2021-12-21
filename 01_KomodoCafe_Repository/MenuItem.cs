using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_KomodoCafe.Repository
{
    public class MenuItem
    {
        public MenuItem()
        {

        }
        public MenuItem(string mealName, string mealDescription, List<string> ingredients, double price)
        {
            MealName = mealName;
            Description = mealDescription;
            Ingredients = ingredients;
            Price = price;
        }
        public int MealNumber { get; set; }
        public string MealName { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public double Price { get; set; }
    }
}
