using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
using ToDoodle.Data.Model;
using ApplicationUser = Todo.Models.ApplicationUser;

namespace Todo.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);

        Task<TodoItem> GetItemAsync(ApplicationUser user, Guid id);

        Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user);

        Task<bool> MarkDoneAsync(Guid id, ApplicationUser user);

        Task<bool> UpdateItemAsync(TodoItem item);

        Task<bool> DeleteItemAsync(TodoItem item);
    }
}
