using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Todo.Services
{
    public class TodoItemsService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user)
        {
            return await _context.Items.Where(x => x.IsDone == false && x.UserId == user.Id).ToArrayAsync();
        }

        public async Task<TodoItem> GetItemAsync(ApplicationUser user, Guid id)
        {
            return await _context.Items.Where(x => x.ID == id && x.UserId == user.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateItemAsync(TodoItem item)
        {
            _context.Items.Update(item);
            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user)
        {
            newItem.ID = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.UserId = user.Id;

            await _context.Items.AddAsync(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            var item = await _context.Items.Where(x => x.ID == id && x.UserId == user.Id).SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }
    }
}
