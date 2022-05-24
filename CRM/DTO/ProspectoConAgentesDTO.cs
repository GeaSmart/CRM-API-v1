using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTO
{
    public class ProspectoConAgentesDTO: ProspectoDTO
    {
        public List<AgenteDTO> Agentes { get; set; }
    }
}
