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
    [Route("api/subestaciones")] 
    public class SubestacionesController : CustomBaseController
    {
        public readonly ApplicationDbContext context;
        public readonly IMapper mapper;

        public SubestacionesController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
    
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SubestacionCreacionDTO subestacionCreacionDTO)
        { 
            var zonaExiste= await context.Zonas
                  .AnyAsync(x => x.Id == subestacionCreacionDTO.ZonaId);

            if (!zonaExiste)
            {
                return BadRequest("La zona proporcionada no existe");
            }

            var subestacion = mapper.Map<Subestacion>(subestacionCreacionDTO);
            subestacion.ZonaId = subestacionCreacionDTO.ZonaId;

            context.Add(subestacion);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SubestacionCreacionDTO subestacionCreacionDTO)
        {

            return await Put<SubestacionCreacionDTO, Subestacion>(id, subestacionCreacionDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubestacionPatchDTO> patchDocument)
        {
            return await Patch<Subestacion, SubestacionPatchDTO>(id, patchDocument);
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Subestacion>(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<SubestacionDTO>>> Get()
        {
            return await Get<Subestacion, SubestacionDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerSubestacion")]
        public async Task<ActionResult<SubestacionDTO>> Get(int id)
        {
            return await Get<Subestacion, SubestacionDTO>(id);

        }
    }
}
