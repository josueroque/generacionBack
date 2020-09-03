using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Entidades
{
    public class Nivel : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(5)]
        public string Nombre { get; set; }
    }
}
