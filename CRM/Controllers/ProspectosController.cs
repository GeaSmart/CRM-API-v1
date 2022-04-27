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
            var existe = await context.Prospectos.AnyAsync(x => x.Nombre == prospectoCreacionDTO.Nombre);

            if (existe)
                return BadRequest("Ya existe un registro con el mismo nombre.");

            var prospecto = mapper.Map<Prospecto>(prospectoCreacionDTO);
            context.Prospectos.Add(prospecto);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
