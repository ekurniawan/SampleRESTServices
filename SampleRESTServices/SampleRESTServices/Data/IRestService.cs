using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleRESTServices.Data
{
    public interface IRestService
    {
        Task<List<TodoItem>> RefreshDataAsync();
        Task SaveTodoItemAsync(TodoItem item, bool isNewItem);
        Task DeleteTodoItemAsync(string id);
    }
}
