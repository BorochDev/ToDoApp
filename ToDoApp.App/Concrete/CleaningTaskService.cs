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


        //private int id = 0;
        //id ma byc wartoscia globalną dla wszystkich taskow

        public CleaningTaskService()
        {

        }

    }
}
