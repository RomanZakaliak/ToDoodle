using System;
using System.Collections.Generic;
using System.Text;
using Todo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ToDoodle.Data.Model;

namespace Todo.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Models.ApplicationUser>(options)
    {
        public DbSet<TodoItem> Items { get; set; }
        public DbSet<Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
