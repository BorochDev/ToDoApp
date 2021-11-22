using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.App.Abstract
{
    public interface IService <T>
    {
        public List<T> TaskList { get; set; }

        public int AddNewTask(T task);
        public List<T> GetTask(string title);
        public T GetTask(int id);
        public List<T> GetAllTasks();
        public int UpdateTask(int id, string title, string description);
        public void DeleteTask(int id);

    }
}
