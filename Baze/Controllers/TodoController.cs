using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Todo.Services.Interfaces;
using Todo.ViewModels;

using ToDoodle.Data.Model;
using ApplicationUser = Todo.Models.ApplicationUser;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoController(ITodoItemService todoItemService, UserManager<ApplicationUser> userManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var items = await todoItemService.GetIncompleteItemsAsync(currentUser);

            var model = new TodoViewModel()
            {
                Items = items
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> _AddEditItem(Guid id)
        {
            if (id == Guid.Empty)
            {
                return View();
            }

            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var item = await todoItemService.GetItemAsync(currentUser, id);

            return View(item);

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditItem(TodoItem Item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var existedItem = await todoItemService.GetItemAsync(currentUser, Item.ID);

            bool successful = false;
            if (existedItem != null)
            {
                existedItem.Title = Item.Title;
                existedItem.DueAt = Item.DueAt;
                successful = await todoItemService.UpdateItemAsync(existedItem);
            }
            else
            {
                successful = await todoItemService.AddItemAsync(Item, currentUser);
            }

            if(!successful)
            {
                return BadRequest("Could not add/edit item!");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            if(id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var existedItem = await todoItemService.GetItemAsync(currentUser, id);
            
            if(existedItem != null)
            {
                if (await todoItemService.DeleteItemAsync(existedItem))
                    return RedirectToAction("Index");
                else
                    return BadRequest("Cannot delete selected item");
            }
            else
            {
                return BadRequest("Item does not exist!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if(id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await todoItemService.MarkDoneAsync(id, currentUser);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }
    }
}
