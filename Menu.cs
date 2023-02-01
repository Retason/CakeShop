using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop
{
    internal class Menu
    {
       


        List<SecondMenu> items=new List<SecondMenu>();

        public void addItem(SecondMenu secondMenu)
        {
            items.Add(secondMenu);
        }

        public int getItemsCount()
        {
            return items.Count;
        }

        public string getItemName(int index)
        {
            return items[index].name;
        }

        public string[] getItems()
        {
            string[] items_=new string[items.Count];
            for (int i=0;i<items.Count;i++)
            {
                items_[i]=getItemName(i);
            }

            return items_;
        }
        public SecondMenu getItem(int index)
        {
            return items[index];
        }
    }
}
