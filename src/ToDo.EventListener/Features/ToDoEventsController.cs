using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ToDo.EventListener.Features
{
    public class ToDoEventsController : ControllerBase
    {
        private readonly ILogger<ToDoEventsController> _logger;

        public ToDoEventsController(ILogger<ToDoEventsController> logger)
        {
            _logger = logger;
        }

        [Topic("completed")]
        [HttpPost("completed")]
        public IActionResult OnToDoItemCompleted(ToDoItemCompletedEvent @event)
        {
            _logger.LogInformation(
                "ToDo {itemId} (\"{itemTitle}\") from list {listId} was completed",
                @event.ItemId,
                @event.Title,
                @event.ListId);

            return Ok();
        }
    }
}