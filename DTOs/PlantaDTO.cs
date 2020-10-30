using GeneracionAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class PlantaDTO
    {
        public string Nomenclatura { get; set; }
        public string RotulacionSCADA { get; set; }
        public string Nombre { get; set; }
        public int Nodo { get; set; }
        public int OrigenId { get; set; }
        public bool Intercambio { get; set; }
        public bool RelevanteENEE{get;set;}
        public Origen Origen { get; set; }
        public int TensionId { get; set; }
        public Tension Tension { get; set; }
        public int SubestacionId { get; set; }
        public Subestacion Subestacion { get; set; }
        public int FuenteId { get; set; }
        public Fuente Fuente { get; set; }
    }
}
