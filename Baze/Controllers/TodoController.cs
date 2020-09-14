using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Services;
using Todo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoItemService todoItemService, 
            UserManager<ApplicationUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

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

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var item = await _todoItemService.GetItemAsync(currentUser, id);

            return View(item);

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditItem(TodoItem Item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var existedItem = await _todoItemService.GetItemAsync(currentUser, Item.ID);

            bool successful = false;
            if (existedItem != null)
            {
                existedItem.Title = Item.Title;
                existedItem.DueAt = Item.DueAt;
                successful = await _todoItemService.UpdateItemAsync(existedItem);
            }
            else
            {
                successful = await _todoItemService.AddItemAsync(Item, currentUser);
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

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var existedItem = await _todoItemService.GetItemAsync(currentUser, id);
            
            if(existedItem != null)
            {
                if (await _todoItemService.DeleteItemAsync(existedItem))
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

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _todoItemService.MarkDoneAsync(id, currentUser);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }
    }
}
