using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Entidades
{
    public class Planta:IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        public string Nomenclatura { get; set; }
        public string RotulacionSCADA { get; set; }
        public string RotulacionENEE { get; set; }
        public int Nodo { get; set; }
        public bool SubPlanta { get; set; }
        public bool TieneSubplantas { get; set; }

        public bool Intercambio { get; set; }
        public int OrigenId { get; set; }
        public Origen Origen { get; set; }
        public int TensionId { get; set; }
        public Tension Tension  { get; set; }
        public int SubestacionId { get; set; }
        public Subestacion Subestacion{ get; set; }
        public int FuenteId { get; set; }
        public Fuente Fuente { get; set; }


    }
}
