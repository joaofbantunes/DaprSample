using System;

namespace ToDo.Web.Features
{
    public class ToDoItemCompletedEvent
    {
        public Guid ListId { get; set; }
        
        public Guid ItemId { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}