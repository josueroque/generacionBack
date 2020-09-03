using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Entidades
{
    public class Tension : IId
    {
        public int Id { get; set; }
        [Required]
        public decimal tension { get; set; }
        public int NivelId { get; set; }
        public Nivel Nivel { get; set; }

    }
}
