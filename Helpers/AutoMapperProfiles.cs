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
            
            CreateMap<Subestacion, SubestacionDTO>().ReverseMap();
            CreateMap<SubestacionCreacionDTO, Subestacion>();
            CreateMap<SubestacionPatchDTO, Subestacion>().ReverseMap();

            CreateMap<Fuente, FuenteDTO>().ReverseMap();

            CreateMap<Planta, PlantaDTO>().ReverseMap();
            CreateMap<PlantaCreacionDTO, Planta>();
            CreateMap<PlantaPatchDTO, Planta>().ReverseMap();

            CreateMap<ScadaValor, ScadaValorDTO>().ReverseMap();
            CreateMap<ScadaValorDTO, ScadaValor>();
            CreateMap<IEnumerable<ScadaValor>, ScadaValorDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.FirstOrDefault().Id))
                .ForMember(x => x.PlantaId, x => x.MapFrom(y => y.FirstOrDefault().PlantaId))
                .ForMember(x => x.Sum, x => x.MapFrom(y => y.FirstOrDefault().Valor));

            CreateMap<ScadaValorCreacionDTO, ScadaValor>();

            CreateMap<ComercialDato, ComercialDatoDTO>().ReverseMap();
            CreateMap<ComercialDatoDTO, ComercialDato>();
            CreateMap<IEnumerable<ComercialDato>, ComercialDatoDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.FirstOrDefault().Id))
                .ForMember(x => x.PlantaId, x => x.MapFrom(y => y.FirstOrDefault().PlantaId))
                .ForMember(x => x.Sum, x => x.MapFrom(y => y.FirstOrDefault().Entregado))
                .ForMember(x => x.Sum2, x => x.MapFrom(y => y.FirstOrDefault().Recibido));

            CreateMap<ComercialDatoCreacionDTO, ComercialDato>();


            CreateMap<IdentityUser, UsuarioDTO>();

        }
    }
}
