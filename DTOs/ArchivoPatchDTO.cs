using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class ArchivoPatchDTO
    {
        public int Id { get; set; }
     //   public string nombreArchivo { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public bool SCADA { get; set; }
    }
}
