using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop
{
    internal class SecondMenu
    {
        public string name;
        
        List <(string,int)> items = new List<(string, int)>();
        public SecondMenu(string name, List<(string, int)> items)
        {
           
            this.name = name;
            this.items = items;
        }

        public string getItem(int index)
        {
            string out_str = "";

            out_str = items[index].Item1+" - "+ items[index].Item2.ToString();
            return out_str;
        }
        public int getPrice(int index)
        {
            return items[index].Item2;
        }
        public string[] getItems()
        {
            string[] str_items=new string[items.Count];
            for (int i=0;i<items.Count;i++)
            {
                str_items[i]=getItem(i);
            }
            return str_items;
        }
    }
}
