using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class PlantaPatchDTO
    {
        public string Nombre { get; set; }
        public string Nomenclatura { get; set; }
        public string RotulacionSCADA { get; set; }
        public int Nodo { get; set; }

    }
}
