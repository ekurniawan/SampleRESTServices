using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleRESTServices.Data
{
    public class TodoItemManager
    {
        IRestService restServices;

        public TodoItemManager(IRestService service)
        {
            restServices = service;
        }

        public Task<List<TodoItem>> GetTaskAsync()
        {
            return restServices.RefreshDataAsync();
        }

        public Task SaveTaskAsync(TodoItem item, bool isNewItem = false)
        {
            return restServices.SaveTodoItemAsync(item, isNewItem);
        }

        public Task DeleteTaskAsync(TodoItem item)
        {
            return restServices.DeleteTodoItemAsync(item.ID);
        }

    }
}
