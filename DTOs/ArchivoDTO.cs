using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class ArchivoDTO
    {
        public int Id { get; set; }

        public string Ruta { get; set; }
        [Required]
      //  public string nombreArchivo { get; set; }
        public DateTime fecha { get; set; }
        [Required]
        public bool SCADA { get; set; }
    }
}
