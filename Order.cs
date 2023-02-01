using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop
{
    internal class Order
    {
        public string format;
        public string size;
        public string layerTaste;
        public string layerCount;
        public string glaze;
        public string decor;
        public int price;

        public string getAllInfo()
        {
            string out_str = "";
            if (format != null)
            {
                out_str += " Формат торта: " + format;
            }
            if (size != null)
            {
                out_str += " Размер: " + size;
            }
            if (layerTaste != null)
            {
                out_str += " Вкус коржей: "+layerTaste;
            }
            if (layerCount!=null)
            {
                out_str += " Количество коржей: " + layerCount;
            }
            if (glaze != null)
            {
                out_str += " Глазурь: " + glaze;
            }
            if (decor != null)
            {
                out_str += " Декор: " + decor;
            }
            return out_str;
        }
    }
}
