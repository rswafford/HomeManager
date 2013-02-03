using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.RequestModels
{
    public class MovieImportRequestModel
    {
        [Required]
        [MaxLength(300)]
        public string Filename { get; set; }

        [Required]
        [MaxLength(8000)]
        public string FullPath { get; set; }

        [MaxLength(300)]
        public string Title { get; set; }

        [MaxLength(4)]
        public string Year { get; set; }

        public string Format { get; set; }
        public string FileHash { get; set; }
    }
}
