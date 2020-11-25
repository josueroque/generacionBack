using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Entidades
{
    public class CurvaDemandaValor
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set;}
        public int Hora { get; set; }
        public int Minuto { get; set; }
        public float Valor { get; set; }
        public int ArchivoId { get; set;}
        public Archivo Archivo { get; set; }

    }
}
