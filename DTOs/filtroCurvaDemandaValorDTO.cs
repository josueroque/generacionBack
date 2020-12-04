using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class FiltroCurvaDemandaValorDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 10;
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Minuto { get; set; }
        public string Valor{ get; set; }
        public PaginacionDTO Paginacion
        {
            get { return new PaginacionDTO() { Pagina = Pagina, CantidadRegistrosPorPagina = CantidadRegistrosPorPagina }; }
        }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        // public string CampoOrdenar { get; set; }=Null;
        public bool OrderAscendente { get; set; } = false;
    }
}
