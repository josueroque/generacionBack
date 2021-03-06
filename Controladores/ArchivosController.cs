﻿using AutoMapper;
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
using Microsoft.Extensions.Logging;
using GeneracionAPI.Controllers;
using GeneracionAPI.Contexts;
using Microsoft.AspNetCore.Cors;
using System.Data.OleDb;
using Syncfusion.XlsIO;
using Syncfusion.Drawing;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.RegularExpressions;

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

            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.logger = logger;
            this.env = env;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
           
            context.Add(archivo);
            await context.SaveChangesAsync();
            var archivoDTO = mapper.Map<ArchivoDTO>(archivo);
            if (archivoCreacionDTO.SCADA == true)
            {
                await procesarExcel(archivo.Id, nombreArchivo, archivo.Fecha);
            }
            else
            {
                await procesarExcelComercial(archivo.Id, nombreArchivo, archivo.Fecha);
            }
    
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
        public async Task<ActionResult<ArchivoDTO>> Get([FromQuery]  DateTime fecha, bool scada)
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
                .FirstOrDefaultAsync(x => x.Fecha <= fecha && x.Fecha >= fecha && x.SCADA==scada);
            //Mantener comentado solo se usa la primera vez para guardar la curva de demanda a partir de los archivos excel  
            // await   grabarCurvaDemandaInicial();
          //  await grabarInadvertidoInicial();
            if (archivo == null)
            {
                return NotFound();
            }

          return mapper.Map<ArchivoDTO>(archivo);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {

            return await Delete<Archivo>(id);
        }

        private async Task procesarExcel(int id, string nombreArchivo, DateTime fecha)
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


                string[] columnas = new string[] { "A", "B", "C","D","E", "F", "G", "H", "I", "J",
                                                   "K", "L", "M","N","O", "P", "Q", "R", "S", "T",
                                                   "U", "V", "W","X","Y", "Z" ,"AA","AB","AC",
                                                   "AD","AE","AF","AG","AH","AI","AJ","AK","AL",
                                                   "AM","AN","AO","AP","AQ","AR","AS","AT","AU",
                                                   "AV","AW","AX","AY","AZ"
                                                  };

                foreach (IWorksheet hoja in workbook.Worksheets)
                {

                    if (hoja.Name != "Curva_Demanda" && hoja.Name != "Inadvertido" && hoja.Name != "Frecuencia")
                    {

                        for (int i = 1; i <= hoja.Columns.Length-1; i++)
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
                                        if (planta.Nombre == "COHESSA" || planta.Nombre == "SOPOSA")
                                        {
                                            valor = (float.Parse(hoja.Range[columnas[i] + j.ToString()].DisplayText)*-1);
                                        }
                                        else
                                        {
                                            valor = float.Parse(hoja.Range[columnas[i] + j.ToString()].DisplayText);
                                        }
                                    }
                                    context.Add(new Entidades.ScadaValor()
                                    {
                                        Valor = valor,
                                        Fecha = fecha,
                                        Hora = Int16.Parse(hoja.Range["A" + j.ToString()].DisplayText),
                                        PlantaId = planta.Id,
                                        ArchivoId = id,

                                    });

                                }
                          
                            }
                        }
                    }
                    
                    if (hoja.Name == "Curva_Demanda")
                    {
                        for (int h = 3; h < hoja.Rows.Length; h++)
                        {
                            context.Add(new  Entidades.CurvaDemandaValor()
                            {
                                Valor = float.Parse(hoja.Range["B" + h.ToString()].DisplayText),
                                Fecha = fecha,
                                Hora = DateTime.Parse (hoja.Range["A" + h.ToString()].CalculatedValue).Hour,
                                Minuto= DateTime.Parse(hoja.Range["A" + h.ToString()].CalculatedValue).Minute,
                                ArchivoId = id,
                            });
                        }
                    }

                    if (hoja.Name == "Inadvertido")
                    {
                        for (int k = 4; k <=27; k++)
                        {
                            context.Add(new Entidades.InadvertidoValor()
                            {
                                AMM= float.Parse(hoja.Range["B" + k.ToString()].DisplayText),
                                UT = float.Parse(hoja.Range["C" + k.ToString()].DisplayText),
                                ENATREL = float.Parse(hoja.Range["D" + k.ToString()].DisplayText),
                                Fecha = fecha,
                                Hora = Int16.Parse(hoja.Range["A" + k.ToString()].CalculatedValue),
                                ArchivoId = id,
                            });
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

        private async Task procesarExcelComercial(int id, string nombreArchivo, DateTime fecha)
        {
            try
            {
                var archivo = context.Archivos
               .FirstOrDefaultAsync(x => x.Id == id).Result;

                ExcelEngine excelEngine = new ExcelEngine();

                IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Excel2013;

                string basePath = env.WebRootPath + @"\archivos\" + nombreArchivo;

                FileStream sampleFile = new FileStream(basePath, FileMode.Open);

                IWorkbook workbook = application.Workbooks.Open(sampleFile);

                var hoja = workbook.Worksheets[0];
                string[] plantasEspeciales = { }; 
                DateTime fechaGlobal = DateTime.Now;
                for (int i =1; i <= hoja.Rows.Length ; i++)
                {

                    var planta = context.Plantas
                    
                    .FirstOrDefaultAsync( x => x.RotulacionENEE.StartsWith(hoja.Range["A" + i].DisplayText)
                                          && x.RotulacionENEE.EndsWith(hoja.Range["A" + i].DisplayText) ).Result;
              
                    if (planta != null )
                    {
                        
                        if (hoja.Range["B" + i].DisplayText != "")

                        {
                                                     
                            DateTime fechaExcel = Convert.ToDateTime(hoja.Range["B" + i].DisplayText);
                            Nullable<float> entregado = 0;
                            Nullable<float> recibido = 0;
                            if (hoja.Range["C" + i].DisplayText != "")
                            {
                                entregado = float.Parse(hoja.Range["C" + i].DisplayText);
                            }
                            else
                            {
                                entregado = null;
                            }

                            if (hoja.Range["D" + i].DisplayText != "")
                            {
                                recibido = float.Parse(hoja.Range["D" + i].DisplayText);
                            }
                            else
                            {
                                recibido = null;
                            }

                                context.Add(new Entidades.ComercialDato()
                                {

                                    Recibido = recibido,
                                    Entregado = entregado,
                                    Fecha = fecha,
                                    Hora = fechaExcel.Hour==0?23: fechaExcel.Hour-1,
                                    PlantaId = planta.Id,
                                    ArchivoId = id,

                                });
                        }
                    }

                }

                await context.SaveChangesAsync(); //operacion 


                //Ojo de Agua
                var plantaOA = context.Plantas
                .FirstOrDefaultAsync(x => x.Nombre == "OJO DE AGUA").Result;

                for (int i = 0; i < 24; i++)
                {

                    var resultOA = hoja.Rows.Where(o => (o.Cells[0].DisplayText == "OJO DE AGUA ETAPA I" || o.Cells[0].DisplayText == "OJO DE AGUA ETAPA II"
                        ) && CheckDate(o.Cells[1].CalculatedValue) == true)
                    .AsQueryable();
                    if (resultOA.ToList().Count > 0)
                    {
                        var resultOA2 = resultOA.Where(x => Convert.ToDateTime(x.Cells[1].CalculatedValue).Hour == i)
                        .GroupBy(o => new
                        {
                            PlantaId = plantaOA.Id,
                     
                        Fecha = fecha,
                            Hora = i == 0 ? 23 : i - 1,

                        })
                    .Select(g => new
                    {
                        g.Key.PlantaId,
                        g.Key.Fecha,
                        g.Key.Hora,
                        SumEntregado = g.Sum(o => o.Cells[2].CalculatedValue != "" ? float.Parse(o.Cells[2].CalculatedValue) : 0),
                        SumRecibido = g.Sum(o => o.Cells[3].CalculatedValue != "" ? float.Parse(o.Cells[3].CalculatedValue) : 0)

                    }).ToList();

                        context.Add(new Entidades.ComercialDato()
                        {
                            Recibido = resultOA2[0].SumRecibido,
                            Entregado = resultOA2[0].SumEntregado,
                            Fecha = fecha,
                            Hora = resultOA2[0].Hora,
                            PlantaId = resultOA2[0].PlantaId,
                            ArchivoId = id,

                        });
                    }
                }

                await context.SaveChangesAsync(); //operacion 

                //PRADOS-SUR
                var plantaPS = context.Plantas
                .FirstOrDefaultAsync(x => x.Nombre == "PRADOS-SUR").Result;

                for (int i = 0; i < 24; i++)
                {

                    var resultPS = hoja.Rows.Where(o => (o.Cells[0].DisplayText == "ENER. SOLARES" || o.Cells[0].DisplayText == "FOTOVOLTAICA SUREÑA"
                        || o.Cells[0].DisplayText == "GEN. ENERGETICAS") && CheckDate(o.Cells[1].CalculatedValue) == true)
                    .AsQueryable();
                    if (resultPS.ToList().Count > 0)
                    {
                        var resultPS2 = resultPS.Where(x => Convert.ToDateTime(x.Cells[1].CalculatedValue).Hour == i)
                       .GroupBy(o => new
                       {
                           PlantaId = plantaPS.Id,
                           Fecha = fecha,
                           Hora = i == 0 ? 23 : i - 1,

                       })
                       .Select(g => new
                       {
                           g.Key.PlantaId,
                           g.Key.Fecha,
                           g.Key.Hora,
                           SumEntregado = g.Sum(o => o.Cells[2].CalculatedValue != "" ? float.Parse(o.Cells[2].CalculatedValue) : 0),
                           SumRecibido = g.Sum(o => o.Cells[3].CalculatedValue != "" ? float.Parse(o.Cells[3].CalculatedValue) : 0)

                       }).ToList();

                        context.Add(new Entidades.ComercialDato()
                        {
                            Recibido = resultPS2[0].SumRecibido,
                            Entregado = resultPS2[0].SumEntregado,
                            Fecha = fecha,
                            Hora = resultPS2[0].Hora,
                            PlantaId = resultPS2[0].PlantaId,
                            ArchivoId = id,

                        });
                    }
                }
                await context.SaveChangesAsync(); //operacion 
                await grabarVaciosComercial(fecha,id);

            }
            catch (Exception ex)
            {
                var prueba = ex;

            }
        }
        private async Task grabarVaciosComercial( DateTime fecha,Int32 archivoId)
        {

            var ComercialDatos = context.ComercialDatos.Where(y => y.Fecha == fecha)
                                 .Select(x => x.PlantaId);
            var plantas = context.Plantas.Where(x => !ComercialDatos.Contains(x.Id) && x.RelevanteENEE==true).ToList();
      
            foreach(var item in plantas){
                for(Int16 i = 0; i < 24;i++)
                {
                    context.Add(new Entidades.ComercialDato()
                    {
                        Recibido = null,
                        Entregado = null,
                        Fecha = fecha,
                        Hora = i,
                        PlantaId = item.Id,
                        ArchivoId = archivoId,

                    });
                }
                
            }
            
            await context.SaveChangesAsync();
        }
        protected bool CheckDate(String date)
        {
        DateTime Temp;
        
        if (DateTime.TryParse(date, out Temp) == true)
            return true;
        else
            return false;
        }

        private async Task grabarCurvaDemandaInicial()
        {
            try { 

                DateTime fecha1 = DateTime.Parse("01/01/2019");
                DateTime fecha2 = DateTime.Parse("31/12/2019");
                var Archivos = context.Archivos.Where(y => y.SCADA == true && y.Fecha >= fecha1 && y.Fecha <= fecha2);

                foreach (Archivo archivoSCADA in Archivos)
                {
                    ExcelEngine excelEngine = new ExcelEngine();

                    IApplication application = excelEngine.Excel;

                    application.DefaultVersion = ExcelVersion.Excel2013;

                    string basePath = "Z:" + @"\wwwroot/" + @"archivos/" + Path.GetFileName(archivoSCADA.Ruta);

                    FileStream currentFile = new FileStream(basePath, FileMode.Open);

                    IWorkbook workbook = application.Workbooks.Open(currentFile);

                    foreach (IWorksheet hoja in workbook.Worksheets)
                    {
                        if (hoja.Name == "Curva de Demanda" || hoja.Name == "Curva_Demanda")
                        {
                            for (int h = 3; h < hoja.Rows.Length; h++)
                            {
                                Nullable<float> Valor = 0;
                                if (hoja.Range["B" + h.ToString()].DisplayText != "")
                                {
                                    Valor = float.Parse(hoja.Range["B" + h.ToString()].DisplayText);
                                }
                                else
                                {
                                    Valor= null;
                                }

                                context.Add(new Entidades.CurvaDemandaValor()
                                {
                                    Valor = Valor,
                                    Fecha = archivoSCADA.Fecha,
                                    Hora =  DateTime.Parse(hoja.Range["A" + h.ToString()].CalculatedValue).Hour ,
                                    Minuto = DateTime.Parse(hoja.Range["A" + h.ToString()].CalculatedValue).Minute,
                                    ArchivoId = archivoSCADA.Id,
                                });
                            }
                        }
                    }

                }
            //    await context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                var ex = error;
            }
        }

        private async Task grabarInadvertidoInicial()
        {
            try
            {

                DateTime fecha1 = DateTime.Parse("01/02/2020");
                DateTime fecha2 = DateTime.Parse("30/11/2020");
                var Archivos = context.Archivos.Where(y => y.SCADA == true && y.Fecha >= fecha1 && y.Fecha <= fecha2);

                foreach (Archivo archivoSCADA in Archivos)
                {
                    ExcelEngine excelEngine = new ExcelEngine();

                    IApplication application = excelEngine.Excel;

                    application.DefaultVersion = ExcelVersion.Excel2013;

                    string basePath = "Z:" + @"\wwwroot/" + @"archivos/" + Path.GetFileName(archivoSCADA.Ruta);

                    FileStream currentFile = new FileStream(basePath, FileMode.Open);

                    IWorkbook workbook = application.Workbooks.Open(currentFile);

                    foreach (IWorksheet hoja in workbook.Worksheets)
                    {
                        if (hoja.Name == "Inadvertido")
                        {
                            for (int h = 4; h <= 27; h++)
                            {

                                context.Add(new Entidades.InadvertidoValor()
                                {
                                    AMM = float.Parse(hoja.Range["B" + h.ToString()].DisplayText),
                                    UT = float.Parse(hoja.Range["C" + h.ToString()].DisplayText),
                                    ENATREL = float.Parse(hoja.Range["D" + h.ToString()].DisplayText),
                                    Hora = Int16.Parse(hoja.Range["A" + h.ToString()].CalculatedValue),
                                    Fecha = archivoSCADA.Fecha,
                                    ArchivoId = archivoSCADA.Id,
                                });
                            }
                        }
                    }

                }
               //     await context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                var ex = error;
            }
        }


    }

}
