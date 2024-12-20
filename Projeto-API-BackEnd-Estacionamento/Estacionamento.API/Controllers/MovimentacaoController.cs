﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovimentacaoController : ControllerBase
{
    private readonly IMovimentacaoService _movimentacaoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public MovimentacaoController(IMovimentacaoService movimentacaoService, ILogger<MovimentacaoController> logger, IMapper mapper)
    {
        _movimentacaoService = movimentacaoService;
        _logger = logger;
        _mapper = mapper;
    }

    #region Veiculos Estacionados

    [HttpGet("Estacionados")]
    [SwaggerOperation(Summary = "Lista todos os veículos estacionados.", Description = "Retorna todos os veículos que estão estacionados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MovimentacaoEstacionamentoDTO>>> VeiculosEstacionados()
    {
        var estacionados = await _movimentacaoService.GetAllEstacionados();

        _logger.LogInformation($"Veiculos listados com sucesso. {DateTime.Now}");
        return Ok(estacionados);
    }

    #endregion

    #region Entrada de Veiculos

    [HttpPost("Entrada/{placa}/{tipoVeiculo}")]
    //[Authorize]
    [SwaggerOperation(Summary = "Registra a entrada de um veículo no estacionamento.", Description = "Adiciona um veículo como estacionado.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RegistrarEntrada(string placa, string tipoVeiculo)
    {
        var registro = await _movimentacaoService.RegistrarEntrada(placa, tipoVeiculo);

        return registro is null ? NotFound() : Ok(registro);
    }

    #endregion

    #region Saida de veiculos

    [HttpDelete("Saida/{id}/{placa}")]
    //[Authorize]
    [SwaggerOperation(Summary = "Faz a retirada de um veículo estacionado.", Description = "Retira um veículo do estacionamento.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimentacaoEstacionamentoDTO>> RegistrarSaida(int id, string placa)
    {
        var registro = await _movimentacaoService.RegistrarSaida(id, placa);

        if (registro == null)
        {
            return NotFound();
        }

        return Ok(registro);
    }

    #endregion
}
