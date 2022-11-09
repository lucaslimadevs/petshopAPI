using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Cliente")]
    public class Cliente
    {
        [Dapper.Contrib.Extensions.Key]
        public int id_cliente { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string cliente_nome { get; set; }
        [Required(ErrorMessage = "Rua é obrigatório")]
        public string cliente_rua { get; set; }
        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string cliente_bairro { get; set; }
        [Required(ErrorMessage = "Número casa/apt é obrigatório")]
        public string cliente_numero { get; set; }
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string cliente_telefone { get; set; }
    }
}
