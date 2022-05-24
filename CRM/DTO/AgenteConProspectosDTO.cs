using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTO
{
    public class AgenteConProspectosDTO : AgenteDTO
    {
        public List<ProspectoDTO> Prospectos { get; set; }
    }
}
