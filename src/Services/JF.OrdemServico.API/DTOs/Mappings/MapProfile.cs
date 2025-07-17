using AutoMapper;
using JF.OrdemServico.API.DTOs.Request.Chamados;
using JF.OrdemServico.API.DTOs.Response.Chamados;
using JF.OrdemServico.API.DTOs.Response.Login;
using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.API.DTOs.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Chamado, ChamadoResponse>();

        CreateMap<(Usuario usuario, string token), LoginResponse>()
            .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.usuario.Id))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.usuario.Nome))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.usuario.Email))
            .ForMember(dest => dest.ClienteIds, opt => opt.MapFrom(src => src.usuario.ClienteUsuarios.Select(c => c.ClienteId)))
            .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.token))
            ;

        #region Request

        CreateMap<CreateChamadoRequest, Chamado>()
            .ForCtorParam("clienteId", opt => opt.MapFrom(src => src.ClienteId))
            ;

        #endregion

        #region Response

        CreateMap<UpdateChamadoRequest, Chamado>();

        #endregion
    }
}