using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Common
{
    public class Person 
        //usunalem abstract
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Tasks> Duties { get; set; }
        //czemu nie moze byc List od T? bo to jest abstrakcyjna klasa a nie interfejs?
        //Zawsze używać będziemy Tasków jeśli chcemy dodać typ generyczny trzeba dodać go przy nazwie klasy

    }
}
