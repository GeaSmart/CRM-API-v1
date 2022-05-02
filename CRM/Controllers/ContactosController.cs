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
    [Route("api/prospectos/{prospectoId:int}/comentarios")] //Ojo con la ruta dependiente
    public class ContactosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public ContactosController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactoDTO>>> Get(int prospectoId)
        {
            var existeProspecto = await context.Prospectos.AnyAsync(x => x.Id == prospectoId);

            if (!existeProspecto)
                return NotFound("El prospecto no existe");

            var contactos = await context.Contactos.Where(x => x.ProspectoId == prospectoId).ToListAsync();

            return mapper.Map<List<ContactoDTO>>(contactos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int prospectoId, ContactoCreacionDTO contactoCreacionDTO)
        {
            var existeProspecto = await context.Prospectos.AnyAsync(x => x.Id == prospectoId);

            if (!existeProspecto)
                return NotFound("El prospecto no existe");

            var contacto = mapper.Map<Contacto>(contactoCreacionDTO);
            contacto.ProspectoId = prospectoId;

            context.Contactos.Add(contacto);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
