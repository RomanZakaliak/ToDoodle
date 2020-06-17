using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baze.Models
{
    public class TodoViewModel
    {
        public TodoItem[] Items { get; set; }
    }
}
