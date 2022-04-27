using AutoMapper;
using CRM.DTO;
using CRM.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentesController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public AgentesController(ApplicationDBContext context, IMapper mapper)//inyección de dependencias :)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AgenteDTO>>> Get()
        {
            var agentes = await context.Agentes.ToListAsync();
            return mapper.Map<List<AgenteDTO>>(agentes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AgenteDTO>> Get(int id)
        {
            var agente = await context.Agentes.FirstOrDefaultAsync(x=>x.Id == id);

            if (agente == null)
                return NotFound("Registro no encontrado.");

            return mapper.Map<AgenteDTO>(agente);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AgenteCreacionDTO agenteCreacionDTO)
        {
            var existe = await context.Agentes.AnyAsync(x => x.Nombre == agenteCreacionDTO.Nombre);
            if (existe)            
                return BadRequest($"Ya existe un agente con el nombre {agenteCreacionDTO.Nombre}");

            var agente = mapper.Map<Agente>(agenteCreacionDTO);
            context.Add(agente);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
