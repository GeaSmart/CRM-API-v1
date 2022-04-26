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

        public AgentesController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<Agente>> Get()
        {
            return await context.Agentes.ToListAsync();
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
