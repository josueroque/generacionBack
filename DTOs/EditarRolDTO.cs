using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class EditarRolDTO
    {
        [Required]
        public string UsuarioId { get; set; }
        [Required]
        public string NombreRol { get; set; }
    }
}
