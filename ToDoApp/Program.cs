using System;
using ToDoApp.App.Managers;

namespace ToDoApp
{
    public class Program
    {
        static void Main()
        {
            

            TaskManager taskManager = new TaskManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Dodaj zadanie");
                Console.WriteLine("2) Znajdź zadanie");
                Console.WriteLine("3) Pokaż wszystkie zadania");
                Console.WriteLine("4) Aktualizuj zadanie");
                Console.WriteLine("5) Usuń zadanie");
                Console.WriteLine("6) Zakończ program");
                int.TryParse(Console.ReadLine().ToString(), out int choice);
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        taskManager.Test();
                        break;

                    case 2:
                        taskManager.FindTask();
                        break;

                    case 3:
                        taskManager.ShowAll();
                        break;

                    case 4:
                        taskManager.UpdateTask();
                        break;

                    case 5:
                        taskManager.DeleteTask();
                        break;

                    case 6:
                        return;
                        //Environment.Exit(0);


                    default:
                        Console.WriteLine("Bledne dane!");
                        Console.ReadKey();
                        break;
                }


            }
        }
    }
}
