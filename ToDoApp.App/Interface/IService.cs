using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Enums;

namespace ToDoApp.App.Abstract
{
    public interface IService <T>  // <T,Q> dla 2 zmiennnych typów generycznych
    {
        //private List<T> TaskList { get; set; }

        //public List<Q> PeopleList { get; set; }

        public T GetTask(int id);
        public List<T> GetTask(string title);
        public int DeleteTask(int id);
        public int UpdateTask(int id, string title, string description, CleaningActivities taskType, int performanceTime);
        public int AddNewTask(string title, string description, CleaningActivities taskType);
        public List<T> GetAllTasks();

    }
}
