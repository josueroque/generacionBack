using Microsoft.AspNetCore.Mvc;
using GeneracionAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneracionAPI.Contexts;
using AutoMapper;
using GeneracionAPI.DTOs;
using GeneracionAPI.Entidades;

namespace GeneracionAPI.Controladores
{
    [Route("api/fuentes")]
    public class FuentesController : CustomBaseController
    {

        public readonly ApplicationDbContext context;
        public readonly IMapper mapper;
        public FuentesController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FuenteDTO>>> Get()
        {
            return await Get<Fuente, FuenteDTO>();
        }
    }
}
