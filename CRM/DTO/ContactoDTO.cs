using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTO
{
    public class ContactoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Medio { get; set; }
        public string Descripcion { get; set; }
    }
}
