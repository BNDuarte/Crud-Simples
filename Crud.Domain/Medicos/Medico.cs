using Crud.Domain.Especialidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Domain.Medicos
{
    public class Medico
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "o nome pode ter no máximo 50 caracteres")]
        public string Nome { get; set; }
        public int EspecialidadeId { get; set; }
        public Especialidade Especialidade { get; set; }
    }
}
