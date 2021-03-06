﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GeneracionAPI.Entidades
{
    public class Subestacion:IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }
        [StringLength(10)]
        public string Nomenclatura { get; set; }
        public int ZonaId { get; set; }
        public Zona Zona { get; set; }

    }
}
