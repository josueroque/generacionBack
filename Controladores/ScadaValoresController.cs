using AutoMapper;
using AutoMapper.QueryableExtensions;
using GeneracionAPI.Contexts;
using GeneracionAPI.Controllers;
using GeneracionAPI.DTOs;
using GeneracionAPI.Entidades;
using GeneracionAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GeneracionAPI.Controladores
{
    [Route("api/scadavalores")]
    public class ScadaValoresController : CustomBaseController
    {
        public readonly ApplicationDbContext context;
        public readonly IMapper mapper;
        public ScadaValoresController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScadaValorDTO>>> Get(PaginacionDTO paginacionDTO)
        {
            
            var queryable = context.ScadaValores.Include(x => x.Planta).AsQueryable();
            //queryable = queryable.Where(x => x.PeliculaId == peliculaId);
            paginacionDTO.CantidadRegistrosPorPagina = 200000;
            return await Get<ScadaValor, ScadaValorDTO>( paginacionDTO, queryable);

            // return await Get<ScadaValor, ScadaValorDTO>();


        }


        [HttpGet("filtro")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Filtrar(FiltroScadaValorDTO filtroScadaValorDTO)
        {

            var queryable = context.ScadaValores
                .Include(x => x.Planta)
                .ThenInclude(planta => planta.Subestacion)
                .AsQueryable();
                

            if (filtroScadaValorDTO.FechaInicial.Year >= 2020)
            {
                queryable = queryable.Where(x => x.Fecha >= filtroScadaValorDTO.FechaInicial);
            }

            if (filtroScadaValorDTO.FechaFinal.Year>=2020)
            {
                queryable = queryable.Where(x => x.Fecha <= filtroScadaValorDTO.FechaFinal);
            }

            if (!string.IsNullOrEmpty(filtroScadaValorDTO.NombrePLanta))
            {
                queryable = queryable.Where(x => x.Planta.RotulacionSCADA == filtroScadaValorDTO.NombrePLanta);
            }

            if (!string.IsNullOrEmpty(filtroScadaValorDTO.IdFuente))
            {
                queryable = queryable.Where(x => x.Planta.FuenteId == Int16.Parse(filtroScadaValorDTO.IdFuente));
            }

            if (!string.IsNullOrEmpty(filtroScadaValorDTO.IdZona))
            {
                queryable = queryable.Where(x => x.Planta.Subestacion.ZonaId == Int16.Parse(filtroScadaValorDTO.IdZona));
            }

            if (!string.IsNullOrEmpty(filtroScadaValorDTO.IdTension))
            {
                queryable = queryable.Where(x => x.Planta.TensionId == Int16.Parse(filtroScadaValorDTO.IdTension));
            }
            if (!string.IsNullOrEmpty(filtroScadaValorDTO.IdOrigen))
            {
                queryable = queryable.Where(x => x.Planta.OrigenId == Int16.Parse(filtroScadaValorDTO.IdOrigen));
            }

            var queryable2 = queryable.GroupBy(o => new { o.Planta.Nombre, 
                                                        Fuente=o.Planta.Fuente.Nombre,
                                                        Zona=o.Planta.Subestacion.Zona.Nombre,
                                                        Tension=o.Planta.Tension.Nivel.Nombre,
                                                        Origen=o.Planta.Origen.Nombre
                                                        
                                                          })
                 .Select(g => new
                 {
                     g.Key.Nombre,
                     g.Key.Fuente,
                     g.Key.Zona,
                     g.Key.Tension,
                     g.Key.Origen,
                     Sum = g.Sum(o => o.Valor>0? o.Valor*1000:0),
                 });
                if (filtroScadaValorDTO.totales == true)
                {
                    var valores = await queryable2.ToListAsync();
                    return Ok(valores);
                }

            filtroScadaValorDTO.CantidadRegistrosPorPagina = 200000;

            await HttpContext.InsertarParametrosPaginacion(queryable, filtroScadaValorDTO.CantidadRegistrosPorPagina);

            var scadaValores = await queryable.Paginar(filtroScadaValorDTO.Paginacion).ToListAsync();

            // return mapper.Map<List<ScadaValorDTO>>(scadaValores);
     

        var Fechas = new List<DateTime>();
            foreach (var item in scadaValores)
            {
                int pos = Fechas.IndexOf(item.Fecha);
                if (pos == -1)
                {
                    Fechas.Add(item.Fecha);
                    // the array contains the string and the pos variable
                    // will have its position in the array
                }
            }

            Fechas.Sort();

            var DataCruzada = new List<Object>();

            var Horas = new int[24] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

            foreach (DateTime item in Fechas)
            {
                foreach (int item2 in Horas)

                {
                    var ValoresDiccionario = new Dictionary<String ,Object >();
                    var ValoresPlanta = scadaValores.FindAll(e => e.Fecha == item && e.Hora == item2);
                    var auxiliar =new Object { };
                    ValoresDiccionario.Add("fecha", item.ToString("dd/MM/yyyy"));
                    ValoresDiccionario.Add("hora", item2);
                    foreach (var item3 in ValoresPlanta) 
                    {

                     //   ValoresDiccionario.Add(item3.Planta.RotulacionSCADA,  item3.Valor*1000);
                        if (item3.Valor > 0)
                        {
                            ValoresDiccionario.Add(item3.Planta.Nombre, item3.Valor * 1000);
                        }
                        else
                        {
                            ValoresDiccionario.Add(item3.Planta.Nombre, 0);
                        }
                         
                    }

                    DataCruzada.Add(ValoresDiccionario);

                }

            }

            // return Ok(scadaValores);
            return Ok(DataCruzada);

        }


        [HttpGet("{id:int}", Name = "obtenerScadaValor")]
        public async Task<ActionResult<ScadaValorDTO>> Get(int id)
        {
            return await Get<ScadaValor, ScadaValorDTO>(id);

        }
    }
}
