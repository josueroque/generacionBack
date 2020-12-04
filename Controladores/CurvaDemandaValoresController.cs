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

        [Route("api/curvademanda")]
        public class CurvaDemandaValoresController : CustomBaseController
        {
            public readonly ApplicationDbContext context;
            public readonly IMapper mapper;

            public CurvaDemandaValoresController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

        [HttpGet]
    //    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Filtrar(FiltroCurvaDemandaValorDTO filtroCurvaDemandaValorDTO)
        {
            try {         
            var queryable = context.CurvaDemandaValores
            .AsQueryable();

            if (filtroCurvaDemandaValorDTO.FechaInicial.Year >= 2015)
            {
                queryable = queryable.Where(x => x.Fecha >= filtroCurvaDemandaValorDTO.FechaInicial);
            }

            if (filtroCurvaDemandaValorDTO.FechaFinal.Year >= 2015)
            {
                queryable = queryable.Where(x => x.Fecha <= filtroCurvaDemandaValorDTO.FechaFinal);
            }
            
            var queryable2 = queryable.GroupBy(o => new
            {

                Fecha = o.Fecha,
                Hora = o.Hora,


            })
                 .Select(g => new
                 {
                     g.Key.Fecha,
                     g.Key.Hora,
                     valorMaximo = g.Max(o => o.Valor),
                     valorMinimo = g.Min(o => o.Valor)
                 })
                          
              .OrderBy(y => y.Fecha )
              .ThenBy(z => z.Hora);

            filtroCurvaDemandaValorDTO.CantidadRegistrosPorPagina = 500000;

            await HttpContext.InsertarParametrosPaginacion(queryable2, filtroCurvaDemandaValorDTO.CantidadRegistrosPorPagina);

            var curvaDemandaValores = await queryable2.Paginar(filtroCurvaDemandaValorDTO.Paginacion).ToListAsync();

            return Ok(curvaDemandaValores);
        
        }
            catch(Exception error)
            {
                var error2 = error;
                return NotFound(error2);
            }


        }

    }

}
