using GeneracionAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class InadvertidoValorDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Hora { get; set; }
        public Nullable<float> AMM { get; set; }
        public Nullable<float> UT { get; set; }
        public Nullable<float> ENATREL { get; set; }

    }
}
