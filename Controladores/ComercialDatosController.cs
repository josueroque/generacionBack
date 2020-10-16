using AutoMapper;
using GeneracionAPI.Contexts;
using GeneracionAPI.Controllers;
using GeneracionAPI.DTOs;
using GeneracionAPI.Entidades;
using GeneracionAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace GeneracionAPI.Controladores
{
    [Route("api/comercialdatos")]
    public class ComercialDatosController : CustomBaseController
    {
        public readonly ApplicationDbContext context;
        public readonly IMapper mapper;
        public ComercialDatosController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComercialDatoDTO>>> Get(PaginacionDTO paginacionDTO)
        {

            var queryable = context.ComercialDatos.Include(x => x.Planta).AsQueryable();
            //queryable = queryable.Where(x => x.PeliculaId == peliculaId);
            paginacionDTO.CantidadRegistrosPorPagina = 200000;
            return await Get<ComercialDato, ComercialDatoDTO>(paginacionDTO, queryable);

            // return await Get<ScadaValor, ScadaValorDTO>();
        }


        [HttpGet("filtro")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Filtrar(FiltroComercialDatoDTO filtroComercialDatoDTO)
        {

            var queryable = context.ComercialDatos
                .Include(x => x.Planta)
                .ThenInclude(planta => planta.Subestacion)
                .OrderBy(x => x.Planta.Nombre).ThenBy(x => x.Fecha).ThenBy(x => x.Hora)
                .AsQueryable();
             


            if (filtroComercialDatoDTO.FechaInicial.Year >= 2020)
            {
                queryable = queryable.Where(x => x.Fecha >= filtroComercialDatoDTO.FechaInicial);
            }

            if (filtroComercialDatoDTO.FechaFinal.Year >= 2020)
            {
                queryable = queryable.Where(x => x.Fecha <= filtroComercialDatoDTO.FechaFinal);
            }

            if (!string.IsNullOrEmpty(filtroComercialDatoDTO.NombrePLanta))
            {
                queryable = queryable.Where(x => x.Planta.RotulacionSCADA == filtroComercialDatoDTO.NombrePLanta);
            }

            if (!string.IsNullOrEmpty(filtroComercialDatoDTO.IdFuente))
            {
                queryable = queryable.Where(x => x.Planta.FuenteId == Int16.Parse(filtroComercialDatoDTO.IdFuente));
            }

            if (!string.IsNullOrEmpty(filtroComercialDatoDTO.IdZona))
            {
                queryable = queryable.Where(x => x.Planta.Subestacion.ZonaId == Int16.Parse(filtroComercialDatoDTO.IdZona));
            }

            if (!string.IsNullOrEmpty(filtroComercialDatoDTO.IdTension))
            {
                queryable = queryable.Where(x => x.Planta.TensionId == Int16.Parse(filtroComercialDatoDTO.IdTension));
            }
            if (!string.IsNullOrEmpty(filtroComercialDatoDTO.IdOrigen))
            {
                queryable = queryable.Where(x => x.Planta.OrigenId == Int16.Parse(filtroComercialDatoDTO.IdOrigen));
            }
            if (filtroComercialDatoDTO.totales==true)
            {
                queryable = queryable.Where(x => x.Planta.SubPlanta ==false);
            }
            else
            {
                queryable = queryable.Where(x =>  x.Planta.TieneSubplantas==false );
            }

            //var queryable2 = queryable.GroupBy(o => new {
            //    o.Planta.Nombre,
            //    Fuente = o.Planta.Fuente.Nombre,
            //    Zona = o.Planta.Subestacion.Zona.Nombre,
            //    Tension = o.Planta.Tension.Nivel.Nombre,
            //    Origen = o.Planta.Origen.Nombre

            //})
            //     .Select(g => new
            //     {
            //         g.Key.Nombre,
            //         g.Key.Fuente,
            //         g.Key.Zona,
            //         g.Key.Tension,
            //         g.Key.Origen,
            //         Entregado = g.Sum(o => o.Entregado > 0 ? o.Entregado : 0),
            //         Recibido = g.Sum(o => o.Recibido > 0 ? o.Recibido : 0),

            //     });

            //if (filtroComercialDatoDTO.totales == true)
            //{ 
                 var queryable2 = queryable
                                 .Select(g => new
                                 {
                                     NombrePlanta = g.Planta.Nombre,
                                     Fecha= g.Fecha.ToString("dd/MM/yyyy"),
                                     g.Hora,
                                     g.Entregado,
                                     g.Recibido,
                                     g.Planta,
                                    

                                 });
            var valores = await queryable2.ToListAsync();
                return Ok(valores);
            //}

            //filtroComercialDatoDTO.CantidadRegistrosPorPagina = 200000;

            //await HttpContext.InsertarParametrosPaginacion(queryable, filtroComercialDatoDTO.CantidadRegistrosPorPagina);

            //var comercialDatos = await queryable.Paginar(filtroComercialDatoDTO.Paginacion).ToListAsync();

            //// return mapper.Map<List<ScadaValorDTO>>(scadaValores);

            //var Fechas = new List<DateTime>();
            //foreach (var item in comercialDatos)
            //{  
            //    int pos = Fechas.IndexOf(item.Fecha);
            //    if (pos == -1)
            //    {
            //        Fechas.Add(item.Fecha);
            //        // the array contains the string and the pos variable
            //        // will have its position in the array
            //    }
            //}

            //Fechas.Sort();

            //var DataCruzada = new List<Object>();

            //var Horas = new int[24] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

            //foreach (DateTime item in Fechas)
            //{
            //    foreach (int item2 in Horas)

            //    {
            //        var ValoresDiccionario = new Dictionary<String, Object>();
            //        var ValoresPlanta = comercialDatos.FindAll(e => e.Fecha == item && e.Hora == item2);
            //        var auxiliar = new Object { };
            //        ValoresDiccionario.Add("fecha", item.ToString("dd/MM/yyyy"));
            //        ValoresDiccionario.Add("hora", item2);
            //        foreach (var item3 in ValoresPlanta)
            //        {

            //            //   ValoresDiccionario.Add(item3.Planta.RotulacionSCADA,  item3.Valor*1000);
            //            if (item3.Recibido> 0)
            //            {
            //                ValoresDiccionario.Add(item3.Planta.Nombre, item3.Recibido);
            //            }
            //            else
            //            {
            //                ValoresDiccionario.Add(item3.Planta.Nombre, 0);
            //            }

            //        }

            //        DataCruzada.Add(ValoresDiccionario);

            //    }

            //}

            //// return Ok(scadaValores);
            //return Ok(DataCruzada);

        }

        [HttpGet("{id:int}", Name = "obtenerComercialDato")]
        public async Task<ActionResult<ComercialDatoDTO>> Get(int id)
        {
            return await Get<ComercialDato, ComercialDatoDTO>(id);

        }
    }
}
