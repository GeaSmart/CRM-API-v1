using AutoMapper;
using CRM.DTO;
using CRM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Aquí van las reglas de mapeo <origen,destino>
            CreateMap<AgenteCreacionDTO, Agente>();
            CreateMap<Agente, AgenteDTO>();
            CreateMap<ProspectoCreacionDTO, Prospecto>();
            CreateMap<Prospecto,ProspectoDTO>();
            CreateMap<ContactoCreacionDTO, Contacto>();
            CreateMap<Contacto, ContactoDTO>();            
        }
    }
}
