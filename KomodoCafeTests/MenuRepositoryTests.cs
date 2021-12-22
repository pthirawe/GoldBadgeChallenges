using _01_KomodoCafe.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KomodoCafeTests
{
    [TestClass]
    public class MenuRepositoryTests
    {
        [TestMethod]
        public void AddItemTest_ShouldReturnTrue()
        {
            List<string> ingredients = new List<string>()
            {
                "Beef",
                "Green Beans",
                "Butter",
                "Potatoes"
            };

            MenuItem newMenuItem = new MenuItem("10oz Ribeye", "Cooked to order", ingredients, 12.99);

            MenuItemRepository repo = new MenuItemRepository();

            bool success = repo.AddMenuItem(newMenuItem);

            Assert.IsTrue(success);
        }

        MenuItemRepository _repo = new MenuItemRepository();

        [TestInitialize]
        public void Arrange()
        {
            List<string> ingredients0 = new List<string>()
            {
                "Beef",
                "Green Beans",
                "Butter",
                "Potatoes"
            };            
            List<string> ingredients1 = new List<string>()
            {
                "Sugar",
                "Spice",
                "Everything Nice",
            };            
            List<string> ingredients2 = new List<string>()
            {
                "Snips",
                "Snails",
                "Puppy Tails",
            };

            MenuItem item0 = new MenuItem("10oz Ribeye", "Cooked to order", ingredients0, 12.99);
            MenuItem item1 = new MenuItem("Lady's Choice", "Perfect in every way", ingredients1, 8.49);
            MenuItem item2 = new MenuItem("Leftovers", "No refunds", ingredients2, 6.99);

            _repo.AddMenuItem(item0);
            _repo.AddMenuItem(item1);
            _repo.AddMenuItem(item2);
        }

        [TestMethod]
        public void GetMenuTest_ShouldReturnAListWithExpectedItems()
        {
            List<MenuItem> items = _repo.GetMenu();
            int expected = 3;
            int actual = items.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMenuItemTest_ShouldReturnSpecifiedItem()
        {
            MenuItem searchItem = _repo.GetMenuItem(1);

            double actual = searchItem.Price;
            double expected = 8.49;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveMenuItemTest_ShouldReturnTrue()
        {
            bool success = _repo.RemoveMenuItem(1);

            Assert.IsTrue(success);
        }
    }
}
