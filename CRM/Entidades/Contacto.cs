using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Entidades
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Medio { get; set; }
        public string Descripcion { get; set; }

        //Propiedades de navegación
        public Prospecto Prospecto { get; set; }
    }
}
