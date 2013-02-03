using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Domain.Entities.Media
{
    public class Image : MediaItem
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public string Description { get; set; }
        public DateTime DateTaken { get; set; }
    }
}
