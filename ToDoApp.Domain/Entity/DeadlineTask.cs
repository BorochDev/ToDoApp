using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;

namespace ToDoApp.Domain.Entity
{
    class DeadlineTask : Tasks
    {
        public DateTime Deadline { get; set; }
    }
}
