using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todoapp.Models
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public string Labels { get; set; }
        public string Description { get; set; }
    }
}
