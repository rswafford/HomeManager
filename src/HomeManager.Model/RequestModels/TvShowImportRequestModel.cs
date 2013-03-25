using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.RequestModels
{
    public class TvShowImportRequestModel
    {
        [Required]
        [MaxLength(300)]
        public string Filename { get; set; }

        [Required]
        [MaxLength(8000)]
        public string FullPath { get; set; }

        public string SeriesName { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }

        public string Format { get; set; }
        public string FileHash { get; set; }

        public bool UpdateExisting { get; set; }
    }
}
