using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.DTOs
{
    public class UsuarioDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
