using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;
using ToDoApp.Domain.Enums;

namespace ToDoApp.App.Abstract
{
    public abstract class TaskService<T> : IService<T> where T : Tasks
    {
        private List<T> TaskList { get; set; }
        private int id = 0;

        public TaskService()
        {
            TaskList = new List<T>();
        }
        public T GetItem(int id)
        {
            foreach (var item in TaskList)
            {
                if (item.TaskID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public List<T> GetItem(string title)
        {
            return TaskList.Where(t => t.Title.Contains(title)).ToList();
        }

        public int DeleteItem(int id)
        {
            if (GetItem(id).TaskID == -1)
            {
                var task = GetItem(id);
                TaskList.Remove(task);
                return id;
            }
            return -1;
        }

        public virtual int UpdateItem(T item)
        {
            if (item.TaskID != null && item.Title != null && item.Description != null)
            {
                var task = GetItem(id);
                task.Title = item.Title;
                task.Description = item.Description;
                return id;
            }
            return -1;
        }

        public int AddNewItem(T task)
        {
            if (task.Title != null && task.Description != null)
            {
                task.TaskID = id++;
                task.CreatedTime = DateTime.Now;
                TaskList.Add(task);
                return task.TaskID;
            }
            return -1;
        }

        public List<T> GetAllItems()
        {
            return TaskList;
        }



    }
}
