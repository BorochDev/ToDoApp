using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.App.Abstract;
using ToDoApp.App.Concrete;
using ToDoApp.Domain.Common;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enums;

namespace ToDoApp.App.Managers
{
    public class TaskManager
    {
        private readonly CleaningTaskService cleaningTaskService;
        private readonly CookingTaskService cookingTaskService;
        private readonly GroceriesTaskService groceriesTaskService;
        private readonly PersonService personService;
        private readonly TaskService<Tasks> taskService;
        private int id = 0;
        List<Tasks> taskList;
        
        

            
        public TaskManager()
        {
            personService = new PersonService();
            cleaningTaskService = new CleaningTaskService();
            cookingTaskService = new CookingTaskService();
            groceriesTaskService = new GroceriesTaskService();
            //czy teraz tu musimy w nawiasach podać Tasks, Persons?:
            //taskService = new TaskService(Tasks, Persons)
            //czy to zainicjalizowanie taskList jest konieczne?
            List<Tasks> taskList = new List<Tasks>();
            taskList.Concat(cleaningTaskService.GetAllItems());
            taskList.Concat(groceriesTaskService.GetAllItems());
            taskList.Concat(cookingTaskService.GetAllItems());
            taskList.OrderBy(n => n.TaskID);
            //czy to tutaj czy poza konstruktorem?

        }

        public void AddTask()
        {
            string title, description;
            int taskID = id++;
            Console.WriteLine("Podaj tytuł");
            title = Console.ReadLine().ToString();
            Console.WriteLine("Podaj opis");
            description = Console.ReadLine().ToString();
            Console.WriteLine("Podaj typ zadania: Sprzątanie, Zakupy, Gotowanie");
            var taskType = Console.ReadLine().ToString();
            switch (taskType)
            {
                case "Sprzątanie":
                    {
                        double area;
                        CleaningActivities cleaningActivity = new();
                        Console.WriteLine("Podaj pole pracy");
                        if (!double.TryParse(Console.ReadLine().ToString(), out area))
                        {
                            Console.WriteLine("Podano błędne dane.");
                            return;
                        }
                        Console.WriteLine("Wybierz rodzaj sprzątania:");
                        int counter = 1;

                        foreach (var item in Enum.GetNames<CleaningActivities>())
                        {
                            Console.WriteLine("Czy wybierasz {0}? Y/N", item.ToString());
                            var typeAnswer = Console.ReadLine().ToString();
                            if (typeAnswer == "Y")
                            {
                                cleaningActivity = (CleaningActivities)Enum.Parse(typeof(CleaningActivities), item);
                                break;
                            }
                            else if (typeAnswer == "N")
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Podano błędne dane");
                                return;
                            }
                        }
                        //if (!int.TryParse(Console.ReadLine().ToString(), out counter))
                        //{
                        //    Console.WriteLine("Podano błędny typ.");
                        //    return;
                        //}
                        //else
                        //{
                        //    if (counter > Enum.GetValues(typeof(CleaningActivities)).Length)
                        //    {
                        //        Console.WriteLine("Podano liczbę spoza zakresu");
                        //        return;
                        //    }

                        //CleaningActivities cleaningActivities = new CleaningActivities();
                        //dlaczego tutaj daliśmy ten assign a nie poniżej?
                        //dlaczego nie podajemy tu nru counteru wziętego z inputu od użytkownika
                        //dlaczego w wysokopoziomowym task managerze robimy szczegolowe Add Task dla klasy CleaningTasks a nie generyczne Add Task?
                        //
                        CleaningTasks cleaningTask = new CleaningTasks()
                        {
                            Title = title,
                            Description = description,
                            CleaningActivity = cleaningActivity,
                            IsCompleted = false,
                            Area = area,
                            TaskID = taskID,
                            TaskType = taskType
                        };


                        //taskType = Console.ReadLine().ToString();
                        cleaningTaskService.AddNewItem(cleaningTask);
                        break;
                    }
                case "Zakupy":
                    {
                        Console.WriteLine("Podaj rodzaj składnika który chcesz kupić");                      
                        foreach (var ingredient in Enum.GetNames<IngredientsEnum>())
                        {
                            Console.WriteLine(ingredient);
                        }
                        string ingredientAnswer = Console.ReadLine().ToString();
                        if (Enum.GetNames<IngredientsEnum>().Contains(ingredientAnswer.ToLower())==false) 
                        {
                            Console.WriteLine("Podanego składnika nie ma na liście");
                            return;
                        }
                        
                        Console.WriteLine("Podaj cenę jednostkową składnika");
                        if (!double.TryParse(Console.ReadLine().ToString(), out double price))
                        {
                            Console.WriteLine("Podano błędne dane.");
                            return;
                        }

                        Console.WriteLine("Podaj ilość sztuk");                        
                        if (!int.TryParse(Console.ReadLine().ToString(), out int amount))
                        {
                            Console.WriteLine("Podano błędne dane.");
                            return;
                        }
                        
                        //groceries code here
                        GroceriesTasks groceriesTask = new GroceriesTasks()
                        {
                            Title = title,
                            Description = description,
                            IsCompleted = false,
                            Price = price,
                            Amount = amount,
                            IngredientName = (IngredientsEnum)Enum.Parse(typeof(IngredientsEnum), ingredientAnswer.ToLower()),
                            TaskID = taskID,
                            TaskType = taskType 
                        };
                        groceriesTaskService.AddNewItem(groceriesTask);
                        break;
                    }
                case "Gotowanie":
                    {
                        Console.WriteLine("Proszę podaj ścieżkę do pliku .txt z przepisem");
                        string path_recipe = Console.ReadLine().ToString();
                        string recipe;
                        try
                        {
                            recipe = File.ReadAllText(path_recipe);
                        }
                        catch
                        {
                            Console.WriteLine("Podano błędną ścieżkę");
                            return;
                        }
                        finally
                        {

                        }
                        //cooking code here
                        CookingTasks cookingTask = new CookingTasks()
                        {
                            Title = title,
                            Description = description,
                            IsCompleted = false,
                            Recipe = recipe,
                            TaskID = taskID,
                            TaskType = taskType
                        };
                        //jak tu dodac item Ingredients, ktory jest ustawiany w setterze klasy CookingTasks
                        cookingTaskService.AddNewItem(cookingTask);
                        break;
                    }

            }

            Console.WriteLine("Dodano taska");
            Console.ReadKey();
        }

        public void ShowAll()
        {
            //Console.WriteLine("Cleaning Tasks: ");
            //foreach (var item in cleaningTaskService.GetAllItems())
            //{
            //    //wypisać wszystkie dane
            //}
            //Console.WriteLine(...);
            //foreach (var item in groceriesTaskService.GetAllItems())
            //{
            //    //wypisać wszystkie dane
            //}
            //foreach (var item in cookingTaskService.GetAllItems())
            //{
            //    //wypisać wszystkie dane
            //}

            

            foreach (var task in taskList)
            {
                Console.WriteLine(@"ID to {0}, typ: {1} tytuł: {2}, opis: {3}, czas procesowania: {4}", 
                    task.TaskID, task.TaskType, task.Title, task.Description, task.TaskPerformanceTime);
                //switch (typeOfVar_tostring)
                //{
                //    case "CleaningTasks":
                //        //jak sprawdzic do ktorej klasy nalezy, jakiego jest typu
                //        {
                //            Console.WriteLine($"Rodzaj sprzatania to {task.CleaningActivity}");
                //            //teraz jak tu ustawić typ taska żeby dziedziczył po ogólnym tasku ale tylko w przypadku jeśli TaskType=Cleaning
                //            break;
                //        }
                //    case "GroceriesTasks":
                //        {
                //            Console.WriteLine($"Lista zakupów obejmuje {task.Amount} {task.IngredientName} po cenie {task.Price} za sztukę, o łącznym koszcie {task.Price * task.Amount}.");
                //            break;
                //        }
                //    case "CookingTasks":
                //        {
                //            Console.WriteLine($"Przepis na danie {task.DishName} wygląda następująco: {task.Recipe} - wykonuje się go z następujących składników: {String.Join(, task.Ingredients)}");
                //            break;
                //        }
                //}



                if (task.IsCompleted)
                {
                    Console.WriteLine("Zadanie zakończone");
                }
                else
                {
                    Console.WriteLine("W trakcie wykonywania");
                }
                Console.WriteLine(@"Dodano {0}/{1}/{2}", task.CreatedTime.Day, task.CreatedTime.Month, task.CreatedTime.Year);
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public void DeleteTask()
        {
            Console.WriteLine("Podaj numer ID taska do usunięcia, lub jego tytuł");
            string deleteVar = Console.ReadLine().ToString();
            if (!int.TryParse(deleteVar, out int id))
            {
                Console.WriteLine("Podano błędne dane");
                Console.ReadKey();
                return;
            }
            cleaningTaskService.DeleteItem(id);
            Console.WriteLine(@"Usunięto taska o numerze {0}", deleteVar);
            Console.ReadKey();
        }

        public void ShowTask()
        {
            Console.WriteLine("Podaj numer ID taska do pokazania, lub jego tytuł");

            var showVar = Console.ReadLine().ToString();
            bool isNumeric;
            List<Tasks> singleElementList;

            if (!int.TryParse(showVar, out int id))
            {
                isNumeric = false;
                singleElementList = taskList.Where(n => n.Title == showVar).Select(n => n).ToList();

                if (singleElementList.Count != 1)
                {
                    Console.WriteLine("Nie znaleziono taska o podanym tytule");
                    Console.ReadKey();
                    return;
                }          
            }
            else
            {
                isNumeric = true;
                singleElementList = taskList.Where(n => n.TaskID == id).Select(n => n).ToList();

                if (singleElementList.Count != 1)
                {
                    Console.WriteLine("Nie znaleziono taska o podanym ID");
                    Console.ReadKey();
                    return;
                }                
            }

            string typeOfTask = singleElementList[0].TaskType;

            switch (typeOfTask)
            {
                case "Sprzątanie":
                    {
                        if (isNumeric == false)
                        {
                            var cleaningTaskList = cleaningTaskService.GetItem(showVar);
                            foreach (var task in cleaningTaskList)
                            {
                                Console.WriteLine(@"Dane taska o numerze ID {0}:/nStworzono: {1}/nTytuł: {2}/nOpis: {3}/nTyp: {4}/nCzas procesowania: {5}/nStatus: {6}
                                    /nRodzaj sprzątania: {7}/nObszar sprzątania: {8}",
                                    task.TaskID, task.CreatedTime, task.Title, task.Description, task.TaskType, task.TaskPerformanceTime, task.IsCompleted? "Wykonano":"W trakcie", 
                                    task.CleaningActivity, task.Area);

                            }
                        }
                        else
                        {
                            var task = cleaningTaskService.GetItem(id);
                            Console.WriteLine(@"Dane taska o numerze ID {0}:/nStworzono: {1}/nTytuł: {2}/nOpis: {3}/nTyp: {4}/nCzas procesowania: {5}/nStatus: {6}
                                    /nRodzaj sprzątania: {7}/nObszar sprzątania: {8}",
                                    task.TaskID, task.CreatedTime, task.Title, task.Description, task.TaskType, task.TaskPerformanceTime, task.IsCompleted ? "Wykonano" : "W trakcie", 
                                    task.CleaningActivity, task.Area);
                        }
                        break;
                    }
                case "Gotowanie":
                    {
                        if (isNumeric == false)
                        {
                            var cookingTaskList = cookingTaskService.GetItem(showVar);
                            foreach (var task in cookingTaskList)
                            {
                                Console.WriteLine(@"Dane taska o numerze ID {0}:/nStworzono: {1}/nTytuł: {2}/nOpis: {3}/nTyp: {4}/nCzas procesowania: {5}/nStatus: {6}
                                    /nPrzepis: {7}/nLista składników: {8}",
                                    task.TaskID, task.CreatedTime, task.Title, task.Description, task.TaskType, task.TaskPerformanceTime, task.IsCompleted ? "Wykonano" : "W trakcie",
                                    task.Recipe, String.Join(", ",task.Ingredients));

                            }
                        }
                        else
                        {
                            var task = cookingTaskService.GetItem(id);
                            Console.WriteLine(@"Dane taska o numerze ID {0}:/nStworzono: {1}/nTytuł: {2}/nOpis: {3}/nTyp: {4}/nCzas procesowania: {5}/nStatus: {6}
                                    /nPrzepis: {7}/nLista składników: {8}",
                                    task.TaskID, task.CreatedTime, task.Title, task.Description, task.TaskType, task.TaskPerformanceTime, task.IsCompleted ? "Wykonano" : "W trakcie",
                                    task.Recipe, String.Join(", ", task.Ingredients));
                        }
                        break;
                    }
                case "Zakupy":
                    {
                        if (isNumeric == false)
                        {
                            var groceriesTaskList = groceriesTaskService.GetItem(showVar);
                            foreach (var task in groceriesTaskList)
                            {
                                Console.WriteLine(@"Dane taska o numerze ID {0}:/nStworzono: {1}/nTytuł: {2}/nOpis: {3}/nTyp: {4}/nCzas procesowania: {5}/nStatus: {6}
                                    /nRodzaj składnika: {7}/nCena jednostkowa: {8}/nIlość sztuk: {9}/nCałkowity koszt: {10}",
                                    task.TaskID, task.CreatedTime, task.Title, task.Description, task.TaskType, task.TaskPerformanceTime, task.IsCompleted ? "Wykonano" : "W trakcie",
                                    task.IngredientName, task.Price, task.Amount, task.GetTotalCost(task.Amount, task.Price));

                            }
                        }
                        else
                        {
                            var task = groceriesTaskService.GetItem(id);
                            Console.WriteLine(@"Dane taska o numerze ID {0}:/nStworzono: {1}/nTytuł: {2}/nOpis: {3}/nTyp: {4}/nCzas procesowania: {5}/nStatus: {6}
                                    /nRodzaj składnika: {7}/nCena jednostkowa: {8}/nIlość sztuk: {9}/nCałkowity koszt: {10}",
                                    task.TaskID, task.CreatedTime, task.Title, task.Description, task.TaskType, task.TaskPerformanceTime, task.IsCompleted ? "Wykonano" : "W trakcie",
                                    task.IngredientName, task.Price, task.Amount, task.GetTotalCost(task.Amount, task.Price));
                        }
                        break;
                    }                   
                        
                            
                default:
                    {
                        break;
                    }

            }
            Console.ReadKey();
        }

        public void UpdateTask()
        //nie wiem jak tu zmienić tak żeby UpdateItem przyjmowal tylko parametr T item, przeciez musi znalezc najpierw id taska a potem przyjac nowe parametry (nowy tytuł, 
        //nowy opis itp do zmiany. wyjasnijmy to na lekcji
        {
            Console.WriteLine("Podaj numer ID taska do pokazania, lub jego tytuł");
            string updateVar = Console.ReadLine().ToString();            
            bool isNumeric;
            List<Tasks> singleElementList;
            int id;

            if (!int.TryParse(updateVar, out id))
            {
                isNumeric = false;
                singleElementList = taskList.Where(n => n.Title == updateVar).Select(n => n).ToList();

                if (singleElementList.Count != 1)
                {
                    Console.WriteLine("Nie znaleziono taska o podanym tytule");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                isNumeric = true;
                singleElementList = taskList.Where(n => n.TaskID == id).Select(n => n).ToList();

                if (singleElementList.Count != 1)
                {
                    Console.WriteLine("Nie znaleziono taska o podanym ID");
                    Console.ReadKey();
                    return;
                }
            }

            string typeOfTask = singleElementList[0].TaskType;
            
            Console.WriteLine("Podaj nowy tytuł taska");
            string newTitle = Console.ReadLine().ToString();
            Console.WriteLine("Podaj nowy opis taska");
            string newDescription = Console.ReadLine().ToString();
            
            switch (typeOfTask)
            {
                case "Sprzątanie":
                    {
                        if (isNumeric == false)
                        {
                            var cleaningTaskList = cleaningTaskService.GetItem(updateVar);
                                                        
                            Console.WriteLine("Więcej niż jeden task o podanej nazwie: " + updateVar + ". O które ID Ci chodziło?");
                            foreach (var foundTask in cleaningTaskList)
                            {
                                Console.WriteLine(foundTask.TaskID);
                            }
                            if (!int.TryParse(Console.ReadLine().ToString(), out id))
                            {
                                Console.WriteLine("Podano błędne dane");
                                Console.ReadKey();
                                return;
                            }                         
                        }
                        
                        var task = cleaningTaskService.GetItem(id);

                        task.Title = newTitle;
                        task.Description = newDescription;

                        Console.WriteLine("Czy chcesz zmienić rodzaj sprzątania? Y/N");
                        string answer = Console.ReadLine().ToString();
                        if (answer == "Y")
                        {
                            foreach (var item in Enum.GetNames<CleaningActivities>())
                            {
                                Console.WriteLine("Czy wybierasz {0}? Y/N", item.ToString());
                                var typeAnswer = Console.ReadLine().ToString();
                                if (typeAnswer == "Y")
                                {
                                    var cleaningActivity = (CleaningActivities)Enum.Parse(typeof(CleaningActivities), item);
                                    task.CleaningActivity = cleaningActivity;
                                    break;
                                }
                                else if (typeAnswer == "N")
                                {
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("Podano błędne dane");
                                    return;
                                }
                            }
                        }

                        Console.WriteLine("Czy chcesz zmienić powierzchnię? Y/N");
                        answer = Console.ReadLine().ToString();
                        if (answer=="Y")
                        {
                            Console.WriteLine("Podaj nową powierzchnię sprzątania");
                            if (!double.TryParse(Console.ReadLine().ToString(), out double newArea))
                            {
                                Console.WriteLine("Podano błędne dane");
                                Console.ReadKey();
                                return;
                            }
                            task.Area = newArea;
                        }                        
                        break;
                    }
                case "Gotowanie":
                    {
                        if (isNumeric == false)
                        {
                            var cookingTaskList = cookingTaskService.GetItem(updateVar);

                            Console.WriteLine("Więcej niż jeden task o podanej nazwie: " + updateVar + ". O które ID Ci chodziło?");
                            foreach (var foundTask in cookingTaskList)
                            {
                                Console.WriteLine(foundTask.TaskID);
                            }
                            if (!int.TryParse(Console.ReadLine().ToString(), out id))
                            {
                                Console.WriteLine("Podano błędne dane");
                                Console.ReadKey();
                                return;
                            }
                        }

                        var task = cookingTaskService.GetItem(id);

                        task.Title = newTitle;
                        task.Description = newDescription;

                        Console.WriteLine("Czy chcesz załadować nową receptę z pliku? Y/N");
                        string answer = Console.ReadLine().ToString();
                        if (answer == "Y")
                        {
                            Console.WriteLine("Podaj nową ścieżkę pliku");
                            string path_recipe = Console.ReadLine().ToString();
                            string recipe;
                            try
                            {
                                recipe = File.ReadAllText(path_recipe);
                            }
                            catch
                            {
                                Console.WriteLine("Podano błędną ścieżkę");
                                return;
                            }
                            finally
                            {

                            }
                            task.Recipe = recipe;
                        }
                        break;
                    }
                case "Zakupy":
                    {
                        if (isNumeric == false)
                        {
                            var groceriesTaskList = groceriesTaskService.GetItem(updateVar);

                            Console.WriteLine("Więcej niż jeden task o podanej nazwie: " + updateVar + ". O które ID Ci chodziło?");
                            foreach (var foundTask in groceriesTaskList)
                            {
                                Console.WriteLine(foundTask.TaskID);
                            }
                            if (!int.TryParse(Console.ReadLine().ToString(), out id))
                            {
                                Console.WriteLine("Podano błędne dane");
                                Console.ReadKey();
                                return;
                            }
                        }

                        var task = groceriesTaskService.GetItem(id);

                        task.Title = newTitle;
                        task.Description = newDescription;

                        Console.WriteLine("Czy chcesz zmienić rodzaj składnika? Y/N");
                        string answer = Console.ReadLine().ToString();
                        if (answer == "Y")
                        {
                            foreach (var ingredient in Enum.GetNames<IngredientsEnum>())
                            {
                                Console.WriteLine(ingredient);
                            }
                            string ingredientAnswer = Console.ReadLine().ToString();
                            if (Enum.GetNames<IngredientsEnum>().Contains(ingredientAnswer.ToLower()) == false)
                            {
                                Console.WriteLine("Podanego składnika nie ma na liście");
                                return;
                            }
                            task.IngredientName = (IngredientsEnum)Enum.Parse(typeof(IngredientsEnum), ingredientAnswer);
                        }

                        Console.WriteLine("Czy chcesz zmienić ilość sztuk? Y/N");
                        answer = Console.ReadLine().ToString();
                        if (answer == "Y")
                        {
                            Console.WriteLine("Podaj nową powierzchnię sprzątania");
                            if (!int.TryParse(Console.ReadLine().ToString(), out int newAmount))
                            {
                                Console.WriteLine("Podano błędne dane");
                                Console.ReadKey();
                                return;
                            }
                            task.Amount = newAmount;
                        }

                        Console.WriteLine("Czy chcesz zmienić cenę za sztukę? Y/N");
                        answer = Console.ReadLine().ToString();
                        if (answer == "Y")
                        {
                            Console.WriteLine("Podaj nową powierzchnię sprzątania");
                            if (!double.TryParse(Console.ReadLine().ToString(), out double newPrice))
                            {
                                Console.WriteLine("Podano błędne dane");
                                Console.ReadKey();
                                return;
                            }
                            task.Price = newPrice;
                        }

                        break;
                    }
            }

            if (isNumeric) { Console.WriteLine(@"Udało się zaktualizować dane taska o numerze {0}", id); }
            else { Console.WriteLine(@"Udało się zaktualizować dane taska o tytule {0}", updateVar); }
            
            Console.ReadKey();
        }

        //tu skończyliśmy
        public void CheckEstimatedCompletionTime()
        {
            Console.WriteLine("Podaj id taska:");
            string taskID = Console.ReadLine().ToString();
            if (!int.TryParse(taskID, out int id))
            {
                Console.WriteLine("Podano błędne dane");
                Console.ReadKey();
                return;
            }
            List<Tasks> singleElementList;
            singleElementList = taskList.Where(n => n.TaskID == id).Select(n => n).ToList();

            if (singleElementList.Count != 1)
            {
                Console.WriteLine("Nie znaleziono taska o podanym ID");
                Console.ReadKey();
                return;
            }

            string typeOfTask = singleElementList[0].TaskType;

            switch (typeOfTask)
            {
                case "Sprzątanie":
                    {
                        CleaningTasks cleaningTask = cleaningTaskService.GetItem(id);
                        Console.WriteLine(cleaningTaskService.CheckTaskTime(cleaningTask).ToString());
                        Console.ReadKey();
                        break;
                    }
                case "Gotowanie":
                    {
                        CookingTasks cookingTask = cookingTaskService.GetItem(id);
                        Console.WriteLine(cookingTaskService.CheckTaskTime(cookingTask).ToString());
                        Console.ReadKey();
                        break;
                    }
                case "Zakupy":
                    {
                        GroceriesTasks groceriesTask = groceriesTaskService.GetItem(id);
                        Console.WriteLine(groceriesTaskService.CheckTaskTime(groceriesTask).ToString());
                        Console.ReadKey();
                        break;
                    }
            }

        }

        public void ShowPeople()
        {
            List<Person> peopleList = new List<Person>();
            //zastanawiam się czy w ogóle abstrakcyjna klasa Family Member jest potzebna, chyba chciałbym ją usunąć i zrobić Person normalną klasą
            peopleList = personService.GetAllPeople();
            foreach (var person in peopleList)
            {
                Console.WriteLine(@"{0}, w wieku lat: {1}", person.Name, person.Age);
                foreach (var duty in person.Duties)
                {
                    if (duty.IsCompleted)
                    {
                        Console.WriteLine($"Wykonał zadanie {duty.TaskID}");
                    }
                    else
                    {
                        Console.WriteLine($"Wykonuje zadanie {duty.TaskID}");
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public void AssignTask()
        {
            Console.WriteLine("Podaj imię osoby");
            string personName = Console.ReadLine().ToString();
            Person person = personService.GetPerson(personName);
            bool isNumeric;
            List<Tasks> singleElementList;

            Console.WriteLine("Podaj id lub tytuł taska");
            var taskRef = Console.ReadLine().ToString();
            if (!int.TryParse(taskRef, out id))
            {
                isNumeric = false;
                singleElementList = taskList.Where(n => n.Title == taskRef).Select(n => n).ToList();

                if (singleElementList.Count == 0)
                {
                    Console.WriteLine("Nie znaleziono taska o podanym tytule");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                isNumeric = true;
                singleElementList = taskList.Where(n => n.TaskID == id).Select(n => n).ToList();

                if (singleElementList.Count != 1)
                {
                    Console.WriteLine("Nie znaleziono taska o podanym ID");
                    Console.ReadKey();
                    return;
                }
            }

            if (isNumeric)
            {
                personService.AssignNewTaskToPerson(person, singleElementList[0]);
                Console.WriteLine(@"Dodano taska {0} do osoby {1}", singleElementList[0].Title, person.Name);
            }
            else
            {
                personService.AssignNewTaskToPerson(person, singleElementList);
                Console.WriteLine(@"Dodano {0}elementową listę tasków o tytule {1} do osoby {2}", singleElementList.Count, singleElementList[0].Title, person.Name);
            }          
            
            
        }

        public void AddPerson()
        {
            string name, age;
            Console.WriteLine("Jak ma na imię nowa osoba?");
            name = Console.ReadLine().ToString();
            Console.WriteLine("Ile ma lat?");
            age = Console.ReadLine().ToString();
            if (!int.TryParse(age, out int resultAge))
            {
                Console.WriteLine("Podane błędny wiek, proszę podać liczbę");
                Console.ReadKey();
                return;
            }
            var person = new Person { Name = name, Age = resultAge };
            personService.AddNewPerson(person);
            Console.WriteLine("Dodano osobę");
            Console.ReadKey();
        }

    }
}
