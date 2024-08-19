using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
using ToDoodle.Data.Model;

namespace Todo.ViewModels
{
    public class TodoViewModel
    {
        public TodoItem[] Items { get; set; }
    }
}
