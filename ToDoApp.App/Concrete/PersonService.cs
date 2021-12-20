using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;

namespace ToDoApp.App.Abstract
{
    public class PersonService//<T> : IService<T> where T : Person
        //nie chce jednak zeby to dziedziczylo po IService, bo metody tutaj mają inne parametry wejsciowe (szuka po string name, nie ma property id, wiec nie moze szukac po int id, itd.)
        //usunałęm abstract - skoro Person nie bedzie abstract, to service tez chyba nie powinien byc
    {


        private List<Person> PersonList { get; set; }
        public void AddNewPerson(Person person)
        {
            if (person.Name != null && person.Age>0)
            {
                PersonList.Add(person);
            }
            return;
        }

        public void DeletePerson(string name)
        {
            if (GetPerson(name).Name != null)
            {
                var person = GetPerson(name);
                PersonList.Remove(person);
            }
            return;
        }

        public List<Person> GetAllPeople()
        {
            return PersonList;
        }

        public Person GetPerson(string name)
        {
            foreach (var item in PersonList)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }


        

        public void AssignNewTaskToPerson(Person person, List<Tasks> taskList)
        {
            person.Duties.Concat(taskList);                       
            //dodane po person, nie po id ostatecznie

            //szukanie osoby po id
            //jeśli znajdziesz to do listy dopisujesz task
            //podobnie do deleteTaskFromPerson            
        }

        public void AssignNewTaskToPerson(Person person, Tasks task)
        {
            person.Duties.Add(task);
        }

        public void UnassignTaskFromPerson(Person person, Tasks task)
        {
            person.Duties.Remove(task);
        }

        public List<Tasks> GetPersonsTasks(Person person)
        {
            return person.Duties;
        }
       
    }
}
