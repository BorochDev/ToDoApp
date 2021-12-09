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
        
        public double Time
        {
            get { return Time; }
            set { Time = ((int)CleaningActivity) * Area; }
        }

        public double Area { get; set; }
    }


}
