using AutoMapper;
using GeneracionAPI.Contexts;
using GeneracionAPI.Controllers;
using GeneracionAPI.DTOs;
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

    [Route("api/inadvertido")]
    public class InadvertidoValoresController : CustomBaseController
    {
        public readonly ApplicationDbContext context;
        public readonly IMapper mapper;

        public InadvertidoValoresController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Filtrar(FiltroInadvertidoValorDTO filtroInadvertidoValorDTO)
        {
            try
            {
                var queryable = context.InadvertidoValores
                .AsQueryable();

                if (filtroInadvertidoValorDTO.FechaInicial.Year >= 2015)
                {
                    queryable = queryable.Where(x => x.Fecha >= filtroInadvertidoValorDTO.FechaInicial);
                }

                if (filtroInadvertidoValorDTO.FechaFinal.Year >= 2015)
                {
                    queryable = queryable.Where(x => x.Fecha <= filtroInadvertidoValorDTO.FechaFinal);
                }

                var queryable2 = queryable
                     .Select(g => new
                     {
                         g.Fecha,
                         g.Hora,
                         g.AMM,
                         g.UT,
                         g.ENATREL
                     })

                  .OrderBy(y => y.Fecha)
                  .ThenBy(z => z.Hora);

                filtroInadvertidoValorDTO.CantidadRegistrosPorPagina = 500000;

                await HttpContext.InsertarParametrosPaginacion(queryable2, filtroInadvertidoValorDTO.CantidadRegistrosPorPagina);

                var inadvertidoValores = await queryable2.Paginar(filtroInadvertidoValorDTO.Paginacion).ToListAsync();

                return Ok(inadvertidoValores);

            }
            catch (Exception error)
            {
                var error2 = error;
                return NotFound(error2);
            }

        }

        //[HttpGet("diaria")]
        ////    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> FiltrarDiaria(FiltroInadvertidoValorDTO filtroInadvertidoValorDTO)
        //{
        //    try
        //    {
        //        var queryable = context.InadvertidoValores
        //        .AsQueryable();

        //        if (filtroInadvertidoValorDTO.FechaInicial.Year >= 2015)
        //        {
        //            queryable = queryable.Where(x => x.Fecha >= filtroInadvertidoValorDTO.FechaInicial);
        //        }

        //        if (filtroInadvertidoValorDTO.FechaFinal.Year >= 2015)
        //        {
        //            queryable = queryable.Where(x => x.Fecha <= filtroInadvertidoValorDTO.FechaFinal);
        //        }

        //        var queryable2 = queryable.GroupBy(o => new
        //        {

        //            Fecha = o.Fecha


        //        })

        //             .Select(g => new
        //             {
        //                 g.Key.Fecha,
        //                 AMM = g.Max(o => o.AMM),
        //                 UT = g.Max(o => o.UT),
        //                 ENATREL = g.Max(o => o.ENATREL),
        //             })

        //          .OrderBy(y => y.Fecha);
              

        //        filtroInadvertidoValorDTO.CantidadRegistrosPorPagina = 500000;

        //        await HttpContext.InsertarParametrosPaginacion(queryable2, filtroInadvertidoValorDTO.CantidadRegistrosPorPagina);

        //        var inadvertidoValores = await queryable2.Paginar(filtroInadvertidoValorDTO.Paginacion).ToListAsync();

        //        return Ok(inadvertidoValores);

        //    }
        //    catch (Exception error)
        //    {
        //        var error2 = error;
        //        return NotFound(error2);
        //    }

        //}

    }

}
