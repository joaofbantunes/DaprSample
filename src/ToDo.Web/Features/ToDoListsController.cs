using System;
using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using ToDo.Web.Features;

namespace ToDo.Web.Controllers
{
    [Route("lists")]
    public class ToDoListsController : ControllerBase
    {
        private const string StoreName = "ToDos";

        private readonly DaprClient _daprClient;

        public ToDoListsController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [HttpGet("{toDoList}")]
        public ActionResult<ToDoList> Get([FromState(StoreName)] StateEntry<ToDoList> toDoList)
        {
            // alternatively, could also do
            //var toDoList = await _daprClient.GetStateEntryAsync<ToDoList>(StoreName, toDoListId);

            if (toDoList.Value is null)
            {
                return NotFound();
            }

            return toDoList.Value;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoList>> CreateToDoListAsync()
        {
            var list = new ToDoList {Id = Guid.NewGuid()};
            await _daprClient.SaveStateAsync(StoreName, list.Id.ToString(), list);
            return list;
        }

        [HttpPost("{toDoList}/todos")]
        public async Task<ActionResult<ToDoList>> CreateToDoAsync(
            [FromState(StoreName)] StateEntry<ToDoList> toDoList,
            ToDoItem toDo)
        {
            if (toDoList.Value is null)
            {
                return NotFound();
            }

            toDo.Id = Guid.NewGuid();
            toDoList.Value.ToDos.Add(toDo);
            await toDoList.SaveAsync();
            return toDoList.Value;
        }

        [HttpPost("{toDoList}/todos/{toDoId}/complete")]
        public async Task<ActionResult<ToDoList>> CompleteToDoAsync(
            [FromState(StoreName)] StateEntry<ToDoList> toDoList,
            Guid toDoId)
        {
            var toDo = toDoList.Value?.ToDos.FirstOrDefault(t => t.Id == toDoId);
            if (toDo is null)
            {
                return NotFound();
            }

            toDo.Completed = true;
            await toDoList.SaveAsync();

            await _daprClient.PublishEventAsync(
                "completed",
                new ToDoItemCompletedEvent
                {
                    ListId = toDoList.Value.Id,
                    ItemId = toDoId,
                    Title = toDo.Title,
                    Description = toDo.Description
                });

            return toDoList.Value;
        }
    }
}