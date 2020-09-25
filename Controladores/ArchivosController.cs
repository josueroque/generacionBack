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
using System.Diagnostics;

namespace GeneracionAPI.Controllers
{
    [ApiController]
    [Route("api/archivos")]
    public class ArchivosController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly ApplicationDbContext context2;
        private readonly ApplicationDbContext context3;
        private readonly ApplicationDbContext context4;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly ILogger<ArchivosController> logger;
        private readonly IWebHostEnvironment env;
        private readonly string contenedor = "archivos";
        public ArchivosController(ApplicationDbContext context,
            ApplicationDbContext context2,
            ApplicationDbContext context3,
            ApplicationDbContext context4,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos,
            ILogger<ArchivosController> logger,
            IWebHostEnvironment env)
            : base(context, mapper)
        {
            this.context = context;
            this.context2 = context2;
            this.context3 = context3;
            this.context4 = context4;
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
                    archivo.Ruta =  almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, archivoCreacionDTO.Ruta.ContentType,nombreArchivo).Result;

                }
            }
            //AsignarOrdenActores(pelicula);
            context.Add(archivo);
            await context.SaveChangesAsync();
            var archivoDTO = mapper.Map<ArchivoDTO>(archivo);
            // return NoContent();
           await procesarExcel(archivo.Id, nombreArchivo,archivo.Fecha);
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

        [HttpGet("fecha")]
        public async Task<ActionResult<ArchivoDTO>> Get([FromQuery]  DateTime fecha)
        {
            string mes = fecha.Year.ToString();
            string dia = fecha.Day.ToString();
            if (mes.Length == 1)
            {
                mes = '0' + mes;
            }
            if (dia.Length == 1)
            {
                dia = '0' + dia;
            }
            var fechaCorta = fecha.Date;
            var archivo = await context.Archivos
                .FirstOrDefaultAsync(x => x.Fecha <= fecha && x.Fecha >= fecha);
            if (archivo == null)
            {
                return NotFound();
            }

            //            archivo.PeliculasActores = pelicula.PeliculasActores.OrderBy(x => x.Orden).ToList();

            return mapper.Map<ArchivoDTO>(archivo);
        }

        //[HttpGet("filtro")]
        //public async Task<ActionResult<Archivo>> Filtrar(FiltroArchivoDTO filtroArchivoDTO)
        //{

        //    var queryable = context.Archivos.AsQueryable();

        //    if (filtroArchivoDTO.Fecha.Year >= 2020)
        //    {
        //        queryable = queryable.Where(x => x.Fecha >= filtroArchivoDTO.Fecha && x.Fecha <= filtroArchivoDTO.Fecha);
        //    }

        //    filtroArchivoDTO.CantidadRegistrosPorPagina = 10;
        //    await HttpContext.InsertarParametrosPaginacion(queryable,filtroArchivoDTO.CantidadRegistrosPorPagina);

        //    var archivos = await queryable.Paginar(filtroArchivoDTO.Paginacion).FirstOrDefaultAsync(x => x.SCADA == true);

        //    return mapper.Map<ArchivoDTO>(archivos);

        //}




        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var archivo = await context.Archivos
            //.FirstOrDefaultAsync(x => x.Id == id);
            //if (archivo != null)
            //{
            //     context.Remove(new Entidades.ScadaValor() {ArchivoId = id }); ;
            //}
            return await Delete<Archivo>(id);
        }


        public async Task procesarExcel(int id, string nombreArchivo, DateTime fecha)
        {
            try
            {
                var archivo = context.Archivos
               .FirstOrDefaultAsync(x => x.Id == id).Result;

                if (archivo == null)
                {
                    // return NotFound();
                }

                ExcelEngine excelEngine = new ExcelEngine();

                IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Excel2013;

                string basePath = env.WebRootPath + @"\archivos\" + nombreArchivo;

                FileStream sampleFile = new FileStream(basePath, FileMode.Open);

                IWorkbook workbook = application.Workbooks.Open(sampleFile);

                // IWorksheet worksheet = workbook.Worksheets[0];

                string[] columnas = new string[] { "A", "B", "C","D","E", "F", "G", "H", "I", "J",
                                                   "K", "L", "M","N","O", "P", "Q", "R", "S", "T",
                                                   "U", "V", "W","X","Y", "Z"  };


                //    var plantas = await context.Plantas.ToList();

                foreach (IWorksheet hoja in workbook.Worksheets)
                {

                    var fuente = context.Fuentes
                        .FirstOrDefaultAsync(x => x.Nombre == hoja.Name.Substring(4, hoja.Name.Length - 4)).Result;


                    //      for (int i = 0; i < hoja.Columns.Length; i++)
                    for (int i = 0; i < 3; i++)
                    {

                        var planta = context.Plantas
                            .FirstOrDefaultAsync(x => x.RotulacionSCADA == hoja.Range[columnas[i] + "1"].DisplayText).Result;
                        if (planta != null)
                        {
                            for (int j = 3; j <= 26; j++)
                            {
                                float valor = 0;

                                if ((hoja.Range[columnas[i] + j.ToString()].DisplayText).ToString().Length > 0)
                                {
                                    valor = float.Parse(hoja.Range[columnas[i] + j.ToString()].DisplayText);
                                }
                                context.Add(new Entidades.ScadaValor()
                                {
                                    Valor = valor,
                                    Fecha = fecha,
                                    Hora = Int16.Parse(hoja.Range["A" + j.ToString()].DisplayText),
                                    PlantaId = planta.Id,
                                    ArchivoId=id,
                             

                                });

                            }
                            //string result = context.SaveChangesAsync().Result.ToString();
                        }
                    }
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var prueba = ex;

            }
        }

    }

}
