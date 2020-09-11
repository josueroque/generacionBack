using AutoMapper;
using Microsoft.AspNetCore.Identity;
//using NetTopologySuite.Geometries;
using GeneracionAPI.DTOs;
using GeneracionAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Archivo, ArchivoDTO>().ReverseMap();
            CreateMap<ArchivoCreacionDTO, Archivo>()
                .ForMember(x => x.Ruta, options => options.Ignore())

            ;
            CreateMap<ArchivoPatchDTO, Archivo>().ReverseMap();

        }
    }
}
