using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.ViewModels
{
    public class TodoViewModel
    {
        public TodoItem[] Items { get; set; }
    }
}
