using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Domain.Entity
{
    public class CleaningTasks : Tasks
    {
        public CleaningActivities CleaningActivity { get; set; }
        //czy tu mogę od razu wrzucić enum czy muszę go wrzucać osobno do folderu enums 
        //Lepiej wrzucić do enums

        //public double TaskPerformanceTime
        ////czy jeśli chcę opisać implementacje obowiązkowego property z klasy nadrzędnej, to po prostu deklaruję tak samo te konkretne property w klasie niższej ale już z implementacją?
        //{
        //    get { return TaskPerformanceTime; }
        //    set { TaskPerformanceTime = ((int)CleaningActivity) * Area; }
        //}

        //Virtual i override służą do nadpisywania właściwości i funkcji klas nadrzędnych
        public override double TaskPerformanceTime 
        {
            get { return TaskPerformanceTime; }
            set { TaskPerformanceTime = ((int)CleaningActivity) * Area; }
        }

        
        

        public double Area { get; set; }
    }
}
