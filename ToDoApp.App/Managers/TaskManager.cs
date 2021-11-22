using System;
using ToDoApp.App.Concrete;
using ToDoApp.Domain.Entity;

namespace ToDoApp.App.Managers
{
    public class TaskManager
    {
        private readonly NormalTaskService normalTaskService;
        public TaskManager()
        {
            normalTaskService = new NormalTaskService();
        }


        public void AddTask()
        {
            string title, description;
            Console.Write("Podaj tytuł zadania: ");
            title = Console.ReadLine().ToString();
            Console.Write("Podaj opis zadania: ");
            description = Console.ReadLine().ToString();
            normalTaskService.AddNewTask(new NormalTask() { Title = title, Description = description });
        }

        public void ShowAll()
        {
            foreach (var item in normalTaskService.GetAllTasks())
            {
                Console.WriteLine("ID: " + item.TaskID);
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Description);
                if (item.IsDone)
                {
                    Console.WriteLine("Zadanie skończone");
                }
                else
                {
                    Console.WriteLine("W trakcie wykonywania");
                }
                {

                }
                Console.WriteLine("Dodano: " + item.CreateTime.Day + "." + item.CreateTime.Month + "." + item.CreateTime.Year);
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public void DeleteTask()
        {
            int id = 0;
            Console.Write("Podaj ID zadania do usuniecia: ");
            if (!int.TryParse(Console.ReadLine().ToString(), out id))
            {
                Console.WriteLine("Podano bledne dane!");
                Console.ReadKey();
                return;
            }
            normalTaskService.DeleteTask(id);

        }

        public void FindTask()
        {
            NormalTask task;

            Console.WriteLine("1) szukaj po ID");
            Console.WriteLine("2) szukaj po tytule");
            if (!int.TryParse(Console.ReadLine().ToString(), out int choice))
            {
                Console.Clear();
                Console.WriteLine("Bledne dane!");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            switch (choice)
            {
                case 1:
                    int id;
                    Console.Write("Podaj id: ");
                    if (!int.TryParse(Console.ReadLine().ToString(), out id))
                    {
                        Console.WriteLine("bledne dane!");
                        Console.ReadKey();
                        return;
                    }
                    task = normalTaskService.GetTask(id);

                    if (task != null)
                    {
                        Console.WriteLine("ID: " + task.TaskID);
                        Console.WriteLine(task.Title);
                        Console.WriteLine(task.Description);
                        if (task.IsDone)
                        {
                            Console.WriteLine("Zadanie skończone");
                        }
                        else
                        {
                            Console.WriteLine("W trakcie wykonywania");
                        }
                        {

                        }
                        Console.WriteLine("Dodano: " + task.CreateTime.Day + "." + task.CreateTime.Month + "." + task.CreateTime.Year);

                    }
                    else
                    {
                        Console.WriteLine("Nie znaleziono takiego ID");
                    }

                    Console.ReadKey();

                    break;

                case 2:
                    Console.Write("Podaj tytuł: ");
                    foreach (var item in normalTaskService.GetTask(Console.ReadLine().ToString()))
                    {
                        Console.WriteLine();
                        Console.WriteLine("ID: " + item.TaskID);
                        Console.WriteLine(item.Title);
                        Console.WriteLine(item.Description);
                        if (item.IsDone)
                        {
                            Console.WriteLine("Zadanie skończone");
                        }
                        else
                        {
                            Console.WriteLine("W trakcie wykonywania");
                        }
                        {

                        }
                        Console.WriteLine("Dodano: " + item.CreateTime.Day + "." + item.CreateTime.Month + "." + item.CreateTime.Year);
                        Console.WriteLine();
                    }
                    if (true)
                    {

                    }
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        public void UpdateTask()
        {
            Console.WriteLine("Podaj ID zadania do edycji: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Bledne dane!");
                Console.ReadKey();
                return;
            }
            string title, description;
            Console.Write("Podaj nowy tytuł: ");
            title = Console.ReadLine();
            Console.Write("Podaj nowy opis: ");
            description = Console.ReadLine();

            if (normalTaskService.UpdateTask(id, title, description) != -1)
            {
                Console.Clear();
                Console.WriteLine("Zaktualozowano");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("nie znaleziono zadania o takim ID!");
                Console.ReadKey();
            }

        }

        public void Test()
        {
            int a = 5;
            normalTaskService.Test(out a);
        }
    }
}
