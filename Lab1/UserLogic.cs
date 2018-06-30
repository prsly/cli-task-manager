using System;
using System.Collections.Generic;

namespace Lab1
{
    sealed class UserLogic
    {
        private const string Format = "|{0,5}|{1,30}|{2,5}|";

        BusinessLogic businessLogic = new BusinessLogic();

        List<string> Statuses = new List<string>() { "TODO", "IN PROGRESS", "DONE" };

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
            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine(Statuses[i - 1]);
                Console.WriteLine(Format, "ID", "Задача", "Часы");
                foreach (Task task in DBList)
                {
                    if (task == null) continue;
                    if (task.TaskStatus == i)
                    {
                        string taskName = task.TaskName.Length > 30 ? task.TaskName.Remove(30) : task.TaskName;
                        Console.WriteLine(Format, DBList.IndexOf(task)+1, task.TaskName, task.TaskHours);
                    }

                }
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
                Console.WriteLine("4.Изменить статус задачи");
                Console.WriteLine("5.Просмотреть все задачи");
                Console.WriteLine("6.Сохранить");
                Console.WriteLine("7.Загрузить");
                Console.WriteLine("0.Выход из любого меню");
                
                try
                {
                    k = Convert.ToInt32(Console.ReadLine());
                    if ((k == 2 || k == 3 || k == 4 || k == 5) && businessLogic.DBLength == 0)
                    {
                        Console.WriteLine("Нет задач.\nНажмите any key для продолжения");
                        Console.ReadKey();
                        continue;
                    }
                    if (k < 0 && k > 7)
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
                        Task task = new Task(name, status, hours);
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
                        task = new Task(name, status, hours);
                        businessLogic.Edit(id-1, task);
                        break;
                    case 4:
                        WriteAllDB();
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите id задачи для изменения статуса: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (id >= 0 && id < businessLogic.DBLength + 1)
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
                        List<Task> DBList = businessLogic.ReadAll();
                        name = "";
                        hours = 0;
                        foreach(Task i in DBList)
                        {
                            if (DBList.IndexOf(i)+1 == id)
                            {
                                name = i.TaskName;
                                hours = i.TaskHours;
                            }
                        }
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
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        task = new Task(name, status, hours);
                        businessLogic.Edit(id - 1, task);
                        break;
                    case 5:
                        WriteAllDB();
                        break;
                    case 6:
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
                    case 7:
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