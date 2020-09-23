using AutoMapper;
using GeneracionAPI.Contexts;
using GeneracionAPI.Controllers;
using GeneracionAPI.DTOs;
using GeneracionAPI.Entidades;
using GeneracionAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<List<ScadaValorDTO>>> Filtrar(FiltroScadaValorDTO filtroScadaValorDTO)
        {

            var queryable = context.ScadaValores.Include(x => x.Planta).AsQueryable();

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

            filtroScadaValorDTO.CantidadRegistrosPorPagina = 200000;
            await HttpContext.InsertarParametrosPaginacion(queryable,
                filtroScadaValorDTO.CantidadRegistrosPorPagina);

            var scadaValores = await queryable.Paginar(filtroScadaValorDTO.Paginacion).ToListAsync();

            return mapper.Map<List<ScadaValorDTO>>(scadaValores);

        }


        [HttpGet("{id:int}", Name = "obtenerScadaValor")]
        public async Task<ActionResult<ScadaValorDTO>> Get(int id)
        {
            return await Get<ScadaValor, ScadaValorDTO>(id);

        }
    }
}
