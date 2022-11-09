using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("animal")]
    public class Animal
    {
        [Dapper.Contrib.Extensions.Key]        
        public int id_animal { get; set; }
        [Required(ErrorMessage = "Nome do animal é obrigatório")]
        public string animal_nome { get; set; }
        public string animal_especie { get; set; }
        public string animal_raca { get; set; }
        [Required(ErrorMessage = "Dono é obrigatório")]
        public int animal_fkcliente { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public Cliente fkcliente { get; set; }

    }
}
