﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Common
{
    public abstract class Tasks
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int TaskID { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsCompleted { get; set; }
        //public string? TaskType { get; set; }
        public int TaskPerformanceTime { get; set; }

    }
}
