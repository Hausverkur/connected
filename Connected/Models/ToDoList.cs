using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ApplicationUser Owner { get; set; }

    }
}