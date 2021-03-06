using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTO
{
    public class ProspectoCreacionDTO
    {
        [Required]
        [StringLength(70, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres.")]
        public string Nombre { get; set; }
        public string UrlPerfil { get; set; }
        public List<int> AgentesIds { get; set; }
    }
}
