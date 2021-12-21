using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_KomodoCafe.Repository
{
    public class MenuItemRepository
    {
        private readonly List<MenuItem> _menuList = new List<MenuItem>();

        public bool AddMenuItem(MenuItem newMenuItem)
        {
            if(newMenuItem == null)
            {
                return false;
            }
            newMenuItem.MealNumber = _menuList.Count+1;
            _menuList.Add(newMenuItem);
            return true;
        }
        public List<MenuItem> GetMenu()
        {
            return _menuList;
        }
        public MenuItem GetMenuItem(int index)
        {
            if(index-1 < 0 || index > _menuList.Count)
            {
                return null;
            }
            return _menuList[index-1];
        }
        public bool RemoveMenuItem(int toDelete)
        {
            if (toDelete-1 < 0 || toDelete > _menuList.Count)
            {
                return false;
            }
            _menuList.RemoveAt(toDelete-1);
            foreach(MenuItem item in _menuList)
            {
                item.MealNumber = _menuList.IndexOf(item)+1;
            }
            return true;
        }
    }
}
