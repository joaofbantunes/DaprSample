using System;
using System.Collections.Generic;

namespace ToDo.Web.Features
{
    public class ToDoList
    {
        public Guid Id { get; set; }

        public List<ToDoItem> ToDos { get; set; } = new List<ToDoItem>();
    }
}
