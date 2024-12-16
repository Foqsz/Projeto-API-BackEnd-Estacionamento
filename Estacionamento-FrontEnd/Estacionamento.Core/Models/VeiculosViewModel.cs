﻿using System.ComponentModel.DataAnnotations;

namespace Estacionamento_FrontEnd.Estacionamento.Core.Models
{
    public class VeiculosViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório informar a marca do veículo.")]
        public string Marca { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório informar o modelo do veículo.")]
        public string Modelo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório informar a cor do veículo.")]
        public string Cor { get; set; } = string.Empty;
        [Required(ErrorMessage = "É obrigatório informar a Placa.")]
        public string Placa { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório informar o tipo do veículo.")]
        public string Tipo { get; set; } = string.Empty;
    }
}
