using System;
using System.Collections.Generic;

namespace Lab1
{
    sealed class UserLogic
    {

        private const string Format = "|{0,5}|{1,20}|{2,10}|{3,5}|{4,5}|";

        BusinessLogic businessLogic = new BusinessLogic();

        public void ErrorRead()
        {
            Console.WriteLine("Неверный формат ввода.");
        }

        public void ErrorIDNotFound()
        {
            Console.WriteLine("Такого id не существует.");
        }

        public void WriteAllDB()
        {
            List<Task> DBList = businessLogic.ReadAll();
            foreach (Task task in DBList)
            {
                if (task == null) continue;
               /* string dateBuy = share.DateOfBuy.ToString().Remove(10);
                string companyName = share.CompanyName.Length > 20 ? share.CompanyName.Remove(20) : share.CompanyName;
               Console.WriteLine(Format, DBList.IndexOf(share)+1,
                    share.CompanyName.Length > 20 ? share.CompanyName.Remove(20) : share.CompanyName,
                    dateBuy, share.AmountOfBuy, share.PriceOneOfBuy); */
            }
            DBList.Clear();
        }

        public void Menu()
        {
            int k;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1.Добавить задачу");
                Console.WriteLine("2.Удалить задачу");
                Console.WriteLine("3.Отредактировать задачу");
              //  Console.WriteLine("4.Просмотреть конкретную запись");
                Console.WriteLine("5.Просмотреть все задачи");
              //  Console.WriteLine("6.Показать решение задачи");
                Console.WriteLine("7.Сохранить");
                Console.WriteLine("8.Загрузить");
                Console.WriteLine("0.Выход из любого меню");
                
                try
                {
                    k = Convert.ToInt32(Console.ReadLine());
                    if ((k == 2 || k == 3 || k == 4 || k == 5 || k == 6) && businessLogic.DBLength == 0)
                    {
                        Console.WriteLine("Нет задач.\nНажмите any key для продолжения");
                        Console.ReadKey();
                        continue;
                    }
                    if (k < 0 && k > 9)
                    {
                        Console.WriteLine("Неправильный ввод.\nНажмите any key для продолжения");
                        Console.ReadKey();
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
                
                switch (k)
                {
                    case 1:
                        Console.WriteLine("Введите задачу: ");
                        string name = Console.ReadLine();
                        int status;
                        string statusString = "";
                        while (true)
                        {
                            try
                            { 
                                Console.WriteLine("Выберите статус задачи:" +
                                    "\n1 - TODO" +
                                    "\n2 - IN PROGRESS" +
                                    "\n3 - DONE");
                                status =  Convert.ToInt32(Console.ReadLine());
                                if (status < 1 || status > 3)
                                    throw new Exception();
                                if (status == 1)
                                    statusString = "TODO";
                                if (status == 2)
                                    statusString = "IN PROGRESS";
                                if (status == 3)
                                    statusString = "DONE";
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        int hours;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите количество часов на задачу: ");
                                hours = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        Task task = new Task(name, statusString, hours);
                        businessLogic.Add(task);
                        break;
                    case 2:
                        WriteAllDB();
                        int id;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите id задачи для удаления: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (id >= 0 && id < businessLogic.DBLength+1)
                                {
                                    break;
                                }
                                ErrorIDNotFound();
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        if (id == 0) break;
                        businessLogic.Delete(id-1);
                        break;
                    case 3:
                        WriteAllDB();
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите id задачи для редактирования записи: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (id >= 0 && id < businessLogic.DBLength+1)
                                {
                                    break;
                                }
                                ErrorIDNotFound();
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        if (id == 0) break;
                        Console.WriteLine("Введите задачу: ");
                        name = Console.ReadLine();
                        statusString = "";
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Выберите статус задачи:" +
                                    "\n1 - TODO" +
                                    "\n2 - IN PROGRESS" +
                                    "\n3 - DONE");
                                status = Convert.ToInt32(Console.ReadLine());
                                if (status < 1 || status > 3)
                                    throw new Exception();
                                if (status == 1)
                                    statusString = "TODO";
                                if (status == 2)
                                    statusString = "IN PROGRESS";
                                if (status == 3)
                                    statusString = "DONE";
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите количество часов на задачу: ");
                                hours = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        task = new Task(name, statusString, hours);
                        businessLogic.Edit(id-1, task);
                        break;
                    case 5:
                        WriteAllDB();
                        break;
                    case 7:
                        Console.WriteLine("Введите имя файла: ");
                        string file = Console.ReadLine();
                        if (file == "")
                        {
                            Console.WriteLine("Имя файла введено неверно.");
                            break;
                        }
                        if (businessLogic.SaveFile(file))
                            Console.WriteLine("Файл сохранен.");
                        else
                            Console.WriteLine("Ошибка загрузки.");
                        break;
                    case 8:
                        Console.WriteLine("Введите имя файла:");
                        file = Console.ReadLine();
                        if (file == "")
                        {
                            Console.WriteLine("Имя файла введено неверно.");
                            break;
                        }
                        if (businessLogic.LoadFile(file))
                            Console.WriteLine("Файл загружен.");
                        else
                            Console.WriteLine("Ошибка загрузки.");
                        break;
                    case 0:
                        return;
                }
                Console.WriteLine("Нажмите any key для продолжения");
                Console.ReadKey();
            }
        }
    }

    class Program
    {
        private static void Main()
        {
            UserLogic userLogic = new UserLogic();
            userLogic.Menu();
        }
    }
}