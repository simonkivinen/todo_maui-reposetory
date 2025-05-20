using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace todoapp.Models
{
    public class CategoryItem
    {
        public string Name { get; set; }
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
