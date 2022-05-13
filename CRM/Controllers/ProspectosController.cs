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
    public class ProspectosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public ProspectosController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProspectoDTO>>> Get()
        {
            var prospectos = await context.Prospectos.ToListAsync();
            return mapper.Map<List<ProspectoDTO>>(prospectos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProspectoDTO>> Get(int id)
        {
            var prospecto = await context.Prospectos.FirstOrDefaultAsync(x => x.Id == id);

            if (prospecto == null)
                return NotFound("Registro no encontrado.");

            return mapper.Map<ProspectoDTO>(prospecto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProspectoCreacionDTO prospectoCreacionDTO)
        {
            if (prospectoCreacionDTO.AgentesIds == null)
                return BadRequest("No se puede insertar un prospecto sin asignarle al menos un agente");

            //obtengo la intersección entre ids recibidos e ids de la base de datos
            var agentesIds = await context.Agentes.Where(x => prospectoCreacionDTO.AgentesIds.Contains(x.Id)).Select(x => x.Id).ToListAsync();

            //Con esto me aseguro que los ids que nos envíen realmente existan
            if (agentesIds.Count != prospectoCreacionDTO.AgentesIds.Count)
                return BadRequest("Se ingresó al menos un agente que no existe");

            var prospecto = mapper.Map<Prospecto>(prospectoCreacionDTO);

            AsignarOrdenAgentes(prospecto);

            context.Prospectos.Add(prospecto);
            await context.SaveChangesAsync();
            return Ok();
        }

        private void AsignarOrdenAgentes(Prospecto prospecto)
        {
            for (int i = 0; i < prospecto.AgentesProspectos.Count; i++)
                prospecto.AgentesProspectos[i].Orden = i;
        }
    }
}
