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

        public AgentesController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<AgenteDTO>> Get()
        {
            var agentes = await context.Agentes.ToListAsync();
            return mapper.Map<List<AgenteDTO>>(agentes);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Agente agente)
        {
            var existe = await context.Agentes.AnyAsync(x => x.Nombre == agente.Nombre);
            if (existe)            
                return BadRequest($"Ya existe un agente con el nombre {agente.Nombre}");

            context.Add(agente);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
