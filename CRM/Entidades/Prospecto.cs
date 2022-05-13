using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Entidades
{
    public class Prospecto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres.")]
        public string Nombre { get; set; }
        public string UrlPerfil { get; set; }        

        //Propiedades de navegación
        public List<Contacto> Contactos { get; set; }
        public List<AgenteProspecto> AgentesProspectos { get; set; }
    }
}
