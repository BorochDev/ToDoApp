using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.App.Abstract;
using ToDoApp.Domain.Entity;

namespace ToDoApp.App.Concrete
{
    public class NormalTaskService : IService<NormalTask>
    {
        public List<NormalTask> TaskList { get; set; }
        private int id = 0;

        public NormalTaskService()
        {
            TaskList = new List<NormalTask>();
        }

        public int AddNewTask(NormalTask task)
        {
            if (task.Title != null || task.Description != null)
            {
                task.CreateTime = DateTime.Now;
                task.TaskID = id++;
                TaskList.Add(task);
                return task.TaskID;
            }
            return -1;
        }

        public void DeleteTask(int id)
        {
            TaskList.Remove(GetTask(id));
        }

        public List<NormalTask> GetAllTasks()
        {
            return TaskList;
        }

        public List<NormalTask> GetTask(string title)
        {
            return TaskList.Where(t => t.Title.Contains(title)).ToList();
        }

        public NormalTask GetTask(int id)
        {
            return TaskList.Where(t => t.TaskID == id).FirstOrDefault();
        }

        public int UpdateTask(int id, string title, string description)
        {
            foreach (var item in TaskList)
            {
                if (item.TaskID == id)
                {
                    item.Title = title;
                    item.Description = description;
                    return item.TaskID;
                }
            }
            return -1;
        }

        public bool Test(out int id)
        {
            id = 5;
            return true;
        }

    }
}