using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Entidades
{
    public class AgenteProspecto
    {
        public int AgenteId { get; set; }
        public int ProspectoId { get; set; }
        public int Orden { get; set; }
        public DateTime FechaAsignacion { get; set; }

        //Propiedades de navegación
        public Agente Agente { get; set; }
        public Prospecto Prospecto { get; set; }
    }
}
