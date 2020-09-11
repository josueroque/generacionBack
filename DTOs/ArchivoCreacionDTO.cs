using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GeneracionAPI.Helpers;
using GeneracionAPI.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class ArchivoCreacionDTO:ArchivoPatchDTO
    {
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
      //  [TipoArchivoValidacion(GrupoTipoArchivo.Excel)]
        public IFormFile Ruta { get; set; }

    }
}
