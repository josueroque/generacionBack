using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneracionAPI.DTOs;
using GeneracionAPI.Entidades;
using GeneracionAPI.Servicios;
using GeneracionAPI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using GeneracionAPI.Migrations;
//using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using GeneracionAPI.Controllers;
using GeneracionAPI.Contexts;
using Microsoft.AspNetCore.Cors;
using System.Data.OleDb;
using Syncfusion.XlsIO;
using Syncfusion.Drawing;
using Microsoft.AspNetCore.Hosting;

namespace GeneracionAPI.Controllers
{
    [ApiController]
    [Route("api/archivos")]
    public class ArchivosController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly ILogger<ArchivosController> logger;
        private readonly IWebHostEnvironment env;
        private readonly string contenedor = "archivos";
        public ArchivosController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos,
            ILogger<ArchivosController> logger,
            IWebHostEnvironment env)
            : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.logger = logger;
            this.env = env;
        }


        [HttpPost]
    //    [EnableCors]
        public async Task<ActionResult> Post([FromForm]ArchivoCreacionDTO archivoCreacionDTO)
        {
            var archivo = mapper.Map<Archivo>(archivoCreacionDTO);
            var nombreArchivo = "";
            if (archivoCreacionDTO.Ruta != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await archivoCreacionDTO.Ruta.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(archivoCreacionDTO.Ruta.FileName);
                    nombreArchivo= $"{Guid.NewGuid()}{extension}";
                    archivo.Ruta = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, archivoCreacionDTO.Ruta.ContentType,nombreArchivo);

                }
            }
            //AsignarOrdenActores(pelicula);
            context.Add(archivo);
            await context.SaveChangesAsync();
            var archivoDTO = mapper.Map<ArchivoDTO>(archivo);
            // return NoContent();
            procesarExcel(archivo.Id, nombreArchivo);
           return new CreatedAtRouteResult("obtenerArchivo", new { id = archivo.Id }, archivoDTO);
        }

        [HttpGet("{id}", Name = "obtenerArchivo")]
        public async Task<ActionResult<ArchivoDTO>> Get(int id)
        {
            var archivo = await context.Archivos
                .FirstOrDefaultAsync(x => x.Id == id);
            if (archivo == null)
            {
                return NotFound();
            }

//            archivo.PeliculasActores = pelicula.PeliculasActores.OrderBy(x => x.Orden).ToList();

            return mapper.Map<ArchivoDTO>(archivo);
        }

        [HttpPost("{id}", Name = "aplicarArchivo")]
        public async Task<ActionResult<ArchivoDTO>> Post(int id)
        {
            var archivo = await context.Archivos
                .FirstOrDefaultAsync(x => x.Id == id);
            if (archivo == null)
            {
                return NotFound();
            }

      
            return mapper.Map<ArchivoDTO>(archivo);
        }

        private async void  procesarExcel(int id,string nombreArchivo) {
            var archivo = await context.Archivos
               .FirstOrDefaultAsync(x => x.Id == id);
            if (archivo == null)
            {
               // return NotFound();
            }

            ExcelEngine excelEngine = new ExcelEngine();
                 
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2013;

            string basePath = env.WebRootPath + @"\archivos\" +nombreArchivo;

            FileStream sampleFile = new FileStream(basePath,FileMode.Open);

            IWorkbook workbook = application.Workbooks.Open(sampleFile);

            IWorksheet worksheet = workbook.Worksheets[0];

            foreach (IWorksheet hoja in workbook.Worksheets) { 
                
            }

            //int h = 0;
            //for (int i = 0; i <= worksheet.Rows.Length; i++) {
            //     h = i;
            //}
            var k = worksheet.Range["A15"];

            //worksheet.Columns.Count;
                        


        }
    }
}
