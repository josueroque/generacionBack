using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class ScadaValorTotalesDTO
    {
    
        public int PlantaId { get; set; }
        public float Valor { get; set; }
        public float Sum { get; set; }
    }
}
