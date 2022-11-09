using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("alojamento")]
    public class Alojamento
    {
        [Dapper.Contrib.Extensions.Key]        
        public int id_alojamento { get; set; }
        [Required(ErrorMessage = "descrição é obrigatória")]
        public string alojamento_descricao { get; set; }
        [Required(ErrorMessage = "status é obrigatório")]
        public int alojamento_status { get; set; }

    }
}
