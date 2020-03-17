using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Features
{
    public class ToDoItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Completed { get; set; }
    }
}
