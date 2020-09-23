using AutoMapper;
using GeneracionAPI.Contexts;
using GeneracionAPI.Controllers;
using GeneracionAPI.DTOs;
using GeneracionAPI.Entidades;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Controladores
{
    [Route("api/plantas")]
    public class PlantasController : CustomBaseController
    {
        public readonly ApplicationDbContext context;
        public readonly IMapper mapper;

        public PlantasController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PlantaCreacionDTO plantaCreacionDTO)
        {
            var subestacionExiste = await context.Subestaciones
                  .AnyAsync(x => x.Id == plantaCreacionDTO.SubestacionId );

            if (!subestacionExiste)
            {
                return BadRequest("La subestacion proporcionada no existe");
            }
            
            var origenExiste = await context.Origenes
                 .AnyAsync(x => x.Id == plantaCreacionDTO.OrigenId);

            if (!origenExiste)
            {
                return BadRequest("El origen proporcionado no existe");
            }
            
            var fuenteExiste = await context.Fuentes
                .AnyAsync(x => x.Id == plantaCreacionDTO.FuenteId);

            if (!fuenteExiste)
            {
                return BadRequest("La fuente proporcionada no existe");
            }
            
            var tensionExiste = await context.Tensiones
                .AnyAsync(x => x.Id == plantaCreacionDTO.TensionId);

            if (!tensionExiste)
            {
                return BadRequest("La tension proporcionada no existe");
            }

            var planta= mapper.Map<Planta>(plantaCreacionDTO);

            context.Add(planta);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PlantaCreacionDTO plantaCreacionDTO)
        {

            return await Put<PlantaCreacionDTO, Subestacion>(id, plantaCreacionDTO);
        }

        //[HttpPatch("{id}")]
        //public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubestacionPatchDTO> patchDocument)
        //{
        //    return await Patch<Subestacion, SubestacionPatchDTO>(id, patchDocument);
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Planta>(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<PlantaDTO>>> Get()
        {
            return await Get<Planta, PlantaDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerPlanta")]
        public async Task<ActionResult<PlantaDTO>> Get(int id)
        {
            return await Get<Planta, PlantaDTO>(id);

        }
    }
}
