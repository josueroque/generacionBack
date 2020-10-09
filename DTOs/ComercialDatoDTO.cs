using GeneracionAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GeneracionAPI.DTOs
    {
        public class ComercialDatoDTO
        {
            public int Id { get; set; }
            public float Valor { get; set; }
            public DateTime Fecha { get; set; }
            public int Hora { get; set; }

            public int PlantaId { get; set; }
            public Planta Planta { get; set; }
            public int ArchivoId { get; set; }
            public Archivo Archivo { get; set; }

            public float Sum { get; set; }
            public float Sum2 { get; set; }
    }
    }



