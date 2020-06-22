using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Baze.Models;

namespace Baze.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync();

        Task<bool> AddItemAsync(TodoItem newItem);

        Task<bool> MarkDoneAsync(Guid id);
    }
}
