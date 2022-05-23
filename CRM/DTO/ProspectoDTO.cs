using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTO
{
    public class ProspectoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UrlPerfil { get; set; }
        public List<AgenteDTO> Agentes { get; set; }
    }
}
