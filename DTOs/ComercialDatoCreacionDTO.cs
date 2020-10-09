using GeneracionAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GeneracionAPI.DTOs
{
    public class ComercialDatoCreacionDTO
    {
        public int Id { get; set; }
        public float Recibido { get; set; }
        public float Entregado { get; set; }
        public DateTime Fecha { get; set; }
        public int Hora { get; set; }

        public int PlantaId { get; set; }
        public int ArchivoId { get; set; }
    }
}