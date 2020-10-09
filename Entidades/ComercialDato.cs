using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Entidades
{
    public class ComercialDato:IId
    {
        public int Id { get; set; }
        public Nullable <float> Entregado { get; set; }
        public Nullable <float> Recibido { get; set; }
        public DateTime Fecha { get; set; }
        public int Hora { get; set; }
        public int PlantaId { get; set; }
        public Planta Planta { get; set; }
        public int ArchivoId { get; set; }
        public Archivo Archivo { get; set; }
    }
}
