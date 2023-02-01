using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop
{
    internal class UserInterface // Это основной класс программы, он собирает вместе все остальные
    {
        Order order = new Order();// Класс для формирования заказа
        Menu mainMenu = new Menu();// Главное меню
        int arrowPos = 0;
        int enter_deep=0;// Глубина входа в доп. меню
        void initMenu()
        {
            // Обнуляем всё возможное для функции перезаказа
            order= new Order();
            arrowPos = 0;
            enter_deep = 0;
            mainMenu=new Menu();
            string[] names = new string[7]// Имена подменю
            {"Формат торта",
            "Размер торта",
            "Вкус коржей",
            "Количество коржей",
            "Глазурь",
            "Декор",
            "Конец заказа",
            };

            List<(string, int)> formats = new List<(string, int)>(); // Наполнение подменю. Имя-цена
            formats.Add(("Круг", 500));
            formats.Add(("Квадрат", 500));
            formats.Add(("Прямоугольник", 500));
            formats.Add(("Сердечко", 700));


            List<(string, int)> sizes = new List<(string, int)>();

            sizes.Add(("Маленький (Диаметр - 16см, 8 порций", 1000));
            sizes.Add(("Обычный (Диаметр - 18 см, 10 порций)", 1200));
            sizes.Add(("Большой (Диаметр - 28 см, 24 порции", 2000));

            List<(string, int)> layerTaste = new List<(string, int)>();
            layerTaste.Add(("Ванильный", 100));
            layerTaste.Add(("Шоколадный", 100));
            layerTaste.Add(("Карамельный", 150));
            layerTaste.Add(("Ягодный", 200));
            layerTaste.Add(("Кокосовый", 250));

            List<(string, int)> layersCount = new List<(string, int)>();
            layersCount.Add(("1 корж", 200));
            layersCount.Add(("2 коржа", 400));
            layersCount.Add(("3 коржа", 600));
            layersCount.Add(("4 коржа", 800));

            List<(string, int)> glaze = new List<(string, int)>();

            glaze.Add(("Шоколад", 100));
            glaze.Add(("Крем", 100));
            glaze.Add(("Бизе", 150));
            glaze.Add(("Драже", 150));
            glaze.Add(("Ягоды", 200));

            List<(string, int)> decor = new List<(string, int)>();
            decor.Add(("Шоколадная", 150));
            decor.Add(("Ягодная", 150));
            decor.Add(("Кремовая", 150));

           

            List<List<(string, int)>> secondMenuItems = new List<List<(string, int)>>();
            secondMenuItems.Add(formats);
            secondMenuItems.Add(sizes);
            secondMenuItems.Add(layerTaste);
            secondMenuItems.Add(layersCount);
            secondMenuItems.Add(glaze);
            secondMenuItems.Add(decor);

            for (int i = 0; i <names.Length-1; i++)
            {
                
                mainMenu.addItem(new SecondMenu(names[i], secondMenuItems[i]));
            }

            mainMenu.addItem(new SecondMenu("Конец заказа", null)); // Подменю для конца заказа
        }
        private void correctArrowPosChange(ConsoleKeyInfo consoleKeyInfo,int items_count)
        {
            if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
            {
                if (arrowPos - 1 < 0) arrowPos = items_count-1;
                else arrowPos--;
            }

            if (consoleKeyInfo.Key == ConsoleKey.DownArrow)
            {
                if (arrowPos + 1 > items_count - 1) arrowPos = 0;
                else arrowPos++;
            }
        }
        private void correctEnterDeepChange(bool isInc)// isInc - нужно нам добавить единицу или нет
        {
            if (isInc)
            {
                if (enter_deep + 1 <= 1)
                {
                    enter_deep++;
                }
                
            }
            else
            {
                if (enter_deep - 1 >= 0)
                    enter_deep--;
            }
        }
        private void Init()
        {
            initMenu();
        }
        private void makeFile(Order order) // Создание и запись в файл
        {
            StreamWriter streamWriter = new StreamWriter("ordres.txt", true);
                streamWriter.WriteLine("Заказ от " + DateTime.Now.ToShortDateString());
                streamWriter.WriteLine("Заказ " + order.getAllInfo());
                streamWriter.WriteLine("Цена: " + order.price.ToString());
            streamWriter.Close();
        }
            
        private void MenuCylce() // Основной метод программы. Бесконечный цикл с выводом меню
        {
            string[] currentMenuItems=mainMenu.getItems();
            string menu_name = "";
            while(true)
            {

                PrintMenu(currentMenuItems);
                ConsoleKeyInfo consoleKeyInfo= Console.ReadKey();
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        correctArrowPosChange(consoleKeyInfo, currentMenuItems.Length);
                        break;
                    case ConsoleKey.DownArrow:
                        correctArrowPosChange(consoleKeyInfo, currentMenuItems.Length);
                        break;
                    case ConsoleKey.Enter:
                        if (enter_deep == 0)// 0-Гл.меню
                        {
                            if (arrowPos == mainMenu.getItemsCount() - 1)
                            {
                                makeFile(order);
                                Console.Clear();
                                Console.WriteLine("Спасибо за заказ! Если хотите сделать ещё один, нажмите Enter");
                                ConsoleKeyInfo key= Console.ReadKey();
                                if (key.Key == ConsoleKey.Enter)
                                {
                                    Init();
                                }
                                else
                                {
                                    Environment.Exit(0);
                                }
                            }
                            else
                            {
                                correctEnterDeepChange(true);
                                currentMenuItems = mainMenu.getItem(arrowPos).getItems();// Подгружаем в меню список новых элементов
                                menu_name = mainMenu.getItemName(arrowPos);// Задаём имя текущего доп. меню
                                arrowPos = 0;
                            }
                        }
                        else
                        if (enter_deep == 1)// 1-Доп. меню
                        {
                            switch (menu_name)// Данные заказа записываются в зависимости от выбранного меню
                            {
                                case "Формат торта":
                                    order.format = currentMenuItems[arrowPos];
                                    order.price += mainMenu.getItem(0).getPrice(arrowPos);
                                    break;
                                case "Размер торта":
                                    order.size = currentMenuItems[arrowPos];
                                    order.price += mainMenu.getItem(1).getPrice(arrowPos);
                                    break;
                                case "Вкус коржей":
                                    order.layerTaste = currentMenuItems[arrowPos];
                                    order.price += mainMenu.getItem(2).getPrice(arrowPos);
                                    break;
                                case "Количество коржей":
                                    order.layerCount = currentMenuItems[arrowPos];
                                    order.price+= mainMenu.getItem(3).getPrice(arrowPos);
                                    break;
                                case "Глазурь":
                                    order.glaze = currentMenuItems[arrowPos];
                                    order.price += mainMenu.getItem(4).getPrice(arrowPos);
                                    break;
                                case "Декор":
                                    order.decor = currentMenuItems[arrowPos];
                                    order.price += mainMenu.getItem(5).getPrice(arrowPos);
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        correctEnterDeepChange(false);
                        currentMenuItems = mainMenu.getItems();
                        menu_name = "";
                        break;
                }
               
            }
        }
        private void PrintMenu(string[] items)
        {
            Console.Clear();
            Console.WriteLine("Заказ тортов в Столице тортов");
            Console.WriteLine("Выберите параметр торта");
            for (int i = 0; i < items.Length; i++)
            {
                if (arrowPos == i)
                {
                    Console.WriteLine("->" + items[i]);
                }
                else
                {
                    Console.WriteLine("  " + items[i]);
                }
            }
            Console.WriteLine(enter_deep);
            Console.WriteLine(order.getAllInfo());
            Console.WriteLine("Цена: " + order.price);

        }
        public void startWok()
        {
            MenuCylce();
        }// Запускает работу программы
        public UserInterface()
        {
            Init();
        }
    }
}
