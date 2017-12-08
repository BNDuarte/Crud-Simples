using System.ComponentModel.DataAnnotations;

namespace Crud.Domain.Especialidades
{
    public class Especialidade
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "A especialidade pode ter no máximo 100 caracteres")]
        public string Descricao { get; set; }
    }
}
