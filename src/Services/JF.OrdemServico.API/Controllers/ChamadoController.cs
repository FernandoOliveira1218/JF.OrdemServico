using JF.OrdemServico.API.DTOs.Request.Chamados;
using JF.OrdemServico.API.DTOs.Response.Chamados;
using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JF.OrdemServico.Services.Api.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ChamadoController : ApiControllerBase
{
    private readonly IChamadoService _chamadoService;

    public ChamadoController(IServiceProvider serviceProvider, IChamadoService chamadoService) : base(serviceProvider)
    {
        _chamadoService = chamadoService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var chamado = await _chamadoService.GetByIdAsync(id);

        var dto = _mapper.Map<ChamadoResponse>(chamado);

        return ResponseResult(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var chamados = await _chamadoService.GetAllAsync();

        var dtos = _mapper.Map<IEnumerable<ChamadoResponse>>(chamados);

        return ResponseResult(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CreateChamadoRequest request)
    {
        var entity = _mapper.Map<Chamado>(request);

        await _chamadoService.CreateAsync(entity);

        var dto = _mapper.Map<ChamadoResponse>(entity);

        return ResponseCreated(dto, "Chamado criado com sucesso");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateChamadoRequest request)
    {
        // Copia o ID da rota para o DTO
        var dtoComId = request with { Id = id };

        var chamado = _mapper.Map<Chamado>(dtoComId);

        var atualizado = await _chamadoService.UpdateAsync(chamado);
        var dto = _mapper.Map<ChamadoResponse>(atualizado);

        return ResponseResult(dto, "Chamado atualizado com sucesso");
    }

    [HttpPatch("{id:guid}/finalizar")]
    public async Task<IActionResult> Finalizar(Guid id, [FromBody] FinalizarChamadoRequest request)
    {
        var finalizado = await _chamadoService.FinalizarAsync(id, request.Observacoes, request.DataFinalizacao);

        var dto = _mapper.Map<ChamadoResponse>(finalizado);

        return ResponseResult(dto, "Chamado finalizado com sucesso");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var sucesso = await _chamadoService.RemoveAsync(id);

        return sucesso ? ResponseDeleted("Chamado removido com sucesso") : ResponseFail("Chamado não encontrado", HttpStatusCode.NotFound);
    }
}
