using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class ToDoListTask
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool Status { get; set; }
        public virtual ToDoList List { get; set; }
    }
}
