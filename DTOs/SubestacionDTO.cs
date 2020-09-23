using GeneracionAPI.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class SubestacionDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Nombre { get; set; }
        public string Nomenclatura { get; set; }
        public int ZonaId { get; set; }
        public Zona Zona { get; set; }

    }
}
