using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTO
{
    public class ContactoCreacionDTO
    {
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(25)]
        public string Medio { get; set; }
        [StringLength(250)]
        public string Descripcion { get; set; }
    }
}
