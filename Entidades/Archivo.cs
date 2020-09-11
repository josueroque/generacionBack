using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Entidades
{
    public class Archivo:IId
    {
        public int Id {get;set;}
       [Required]
        public string Ruta { get; set; }
       [Required]          
      // public string nombreArchivo { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        public Boolean SCADA { get; set; }



    }
}
