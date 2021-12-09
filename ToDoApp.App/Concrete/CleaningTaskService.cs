using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.App.Abstract;
using ToDoApp.Domain.Common;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enums;

namespace ToDoApp.App.Concrete
{
    public class CleaningTaskService : TaskService<CleaningTasks>
    {


        private int id = 0;

        public CleaningTaskService()
        {

        }


        public DateTime CheckTime(CleaningTasks cleaningTask)
        {
            DateTime nowTime = DateTime.Now;
            nowTime.AddMinutes(Convert.ToDouble(cleaningTask.TaskPerformanceTime));
            return nowTime;
        }


        //To zrobić w servisie dla rodziny dziedziczącej po PersonService<T> where T:person 

        //public List<FamilyMember> GetAllPeople()
        //{
        //    return PeopleList;
        //}

        //public void AssignPersonToTask(string name, int id)
        //{
        //    FamilyMember person = GetPerson(name);
        //    CleaningTasks cleaningTask = GetItem(id);
        //    if (cleaningTask != null && person != null)
        //    {
        //        person.Duties.Add(cleaningTask);
        //    }
        //}

        //public void AssignPersonToTask(string name, string title)
        //{
        //    FamilyMember person = GetPerson(name);
        //    List<CleaningTasks> setOfTasks = GetItem(title);
        //    if (setOfTasks.Count != 0 && person != null)
        //    {
        //        person.Duties.Concat(setOfTasks);
        //    }
        //}

        //public FamilyMember GetPerson(string name)
        //{
        //    foreach (var person in PeopleList)
        //    {
        //        if (person.Name == name)
        //        {
        //            return person;
        //        }
        //    }
        //    return null;
        //}

        //public void AddNewPerson(string name, int age)
        //{
        //    if (name != null)
        //    {
        //        FamilyMember person = new FamilyMember();
        //        person.Name = name;
        //        person.Age = age;
        //        person.Duties = new List<Tasks>();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Nie podano imienia");
        //        Console.ReadKey();
        //        return;
        //    }
        //}
    }
}
