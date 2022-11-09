using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("prontuario")]
    public class Prontuario
    {
        [Dapper.Contrib.Extensions.Key]        
        public int id_prontuario { get; set; }
        [Required(ErrorMessage = "Motivo é obrigatório")]
        public string prontuario_motivo { get; set; }
        [Required(ErrorMessage = "Alojamento é obrigatório")]
        public int prontuario_fkalojamento { get; set; }
        [Required(ErrorMessage = "Animal é obrigatório")]
        public int prontuario_fkanimal { get; set; }
        [Required(ErrorMessage = "Estado de saúde é obrigatório")]
        public int prontuario_estado { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public Alojamento fkalojamento { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public Animal fkanimal { get; set; }
    }
}
