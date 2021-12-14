using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;

namespace ToDoApp.App.Abstract
{
    public abstract class PersonService<T> : IService<T> where T : Person
    {
        public int AddNewItem(T item)
        {
            throw new NotImplementedException();
        }

        public int DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public T GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetItem(string title)
        {
            throw new NotImplementedException();
        }

        public int UpdateItem(int id, T item)
        {
            throw new NotImplementedException();
        }

        public int AssignNewTaskToPerson(int id, List<Task> task)
        {
            //szukanie osoby po id
            //jeśli znajdziesz to do listy dopisujesz task
            //podobnie do deleteTaskFromPerson
            return -1; //lub id danej osoby
        }
    }
}
