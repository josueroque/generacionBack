using GeneracionAPI.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class PlantaCreacionDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        public string Nomenclatura { get; set; }
        public string RotulacionSCADA { get; set; }
        public bool RelevanteENEE { get; set; }
        public int Nodo { get; set; }
        public int OrigenId { get; set; }

        public int TensionId { get; set; }
       
        public int SubestacionId { get; set; }

        public int FuenteId { get; set; }
   


    }
}
