using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiBackEnd.Models
{
    public class Produto {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Este campo  é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [DataType("VARCHAR(60)")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo  é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Este campo  é obrigatório")]
        [Range(1, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal ValorUnitario { get; set; }
    }
}