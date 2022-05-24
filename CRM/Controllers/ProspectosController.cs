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

        [HttpGet("{id:int}", Name = "ObtenerProspecto")]
        public async Task<ActionResult<ProspectoConAgentesDTO>> Get(int id)
        {
            var prospecto = await context.Prospectos
                .Include(x=>x.AgentesProspectos)
                .ThenInclude(x=>x.Agente)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (prospecto == null)
                return NotFound("Registro no encontrado.");

            prospecto.AgentesProspectos = prospecto.AgentesProspectos.OrderBy(x => x.Orden).ToList(); //ordenando la lista por el campo orden
            return mapper.Map<ProspectoConAgentesDTO>(prospecto);
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

            var prospectoDTO = mapper.Map<ProspectoDTO>(prospecto);
            return CreatedAtRoute("ObtenerProspecto", new { id = prospecto.Id }, prospectoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProspectoCreacionDTO prospectoCreacionDTO)
        {
            var prospecto = await context.Prospectos.Include(x => x.AgentesProspectos).FirstOrDefaultAsync(x => x.Id == id); //así traigo el prospecto y también sus agentes

            if (prospecto == null)
                return NotFound("El prospecto no existe");

            prospecto = mapper.Map(prospectoCreacionDTO, prospecto); //mapeando objetos existentes de origen y destino

            AsignarOrdenAgentes(prospecto);

            context.Prospectos.Update(prospecto);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeProspecto = await context.Prospectos.AnyAsync(x => x.Id == id);
            if (!existeProspecto)
                return NotFound("El prospecto no existe");

            context.Prospectos.Remove(new Prospecto { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private void AsignarOrdenAgentes(Prospecto prospecto)
        {
            for (int i = 0; i < prospecto.AgentesProspectos.Count; i++)
                prospecto.AgentesProspectos[i].Orden = i;
        }
    }
}
