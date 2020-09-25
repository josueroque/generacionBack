using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class ScadaValorCreacionDTO
    {
        
        public float Valor { get; set; }
        public DateTime Fecha { get; set; }
        public int Hora { get; set; }
        public int IdUsuarioGuarda { get; set; }
        public int IdUsuarioModifica { get; set; }
        public int PlantaId { get; set; }
        public int ArchivoId { get; set; }
        public DateTime fechaHoraGuarda { get; set; }
        public DateTime fechaHoraModifica { get; set; }
    }
}
