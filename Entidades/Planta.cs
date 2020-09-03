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
        [StringLength(20)]
        public string Nombre { get; set; }
        [Required]
        public string Nomenclatura { get; set; }
        public string RotulacionSCADA { get; set; }
        public int Nodo { get; set; }
        public int OrigenId { get; set; }
        public Origen Origen { get; set; }
        public int TensionId { get; set; }
        public Tension Tension  { get; set; }
        public int SubestacionId { get; set; }
        public Subestacion Subestacion{ get; set; }
        public int FuenteId { get; set; }
        public Subestacion Fuente { get; set; }


    }
}
