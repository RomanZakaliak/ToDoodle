using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Baze.Data;
using Baze.Models;
using Microsoft.EntityFrameworkCore;

namespace Baze.Services
{
    public class TodoItemsService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            return await _context.Items.Where(x => x.IsDone == false).ToArrayAsync();
        }

        public async Task<bool> AddItemAsync(TodoItem newItem)
        {
            newItem.ID = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);

            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
