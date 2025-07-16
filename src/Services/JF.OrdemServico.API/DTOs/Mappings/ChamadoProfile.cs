using AutoMapper;
using JF.OrdemServico.API.DTOs.Request.Chamados;
using JF.OrdemServico.API.DTOs.Response.Chamados;
using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.API.DTOs.Mappings;

public class ChamadoProfile : Profile
{
    public ChamadoProfile()
    {
        CreateMap<Chamado, ChamadoResponse>();

        CreateMap<CreateChamadoRequest, Chamado>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id gerado no domínio ou serviço
            ;

        CreateMap<UpdateChamadoRequest, Chamado>();
    }
}