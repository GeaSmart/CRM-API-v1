using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTO
{
    public class AgenteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ProspectoDTO> Prospectos { get; set; }
    }
}
