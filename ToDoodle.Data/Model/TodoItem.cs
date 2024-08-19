using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoodle.Data.Model
{
    public class TodoItem
    {
        public Guid ID { get; set; }

        public bool IsDone { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public DateTimeOffset? DueAt { get; set; }

        public string UserId { get; set; }
    }
}
