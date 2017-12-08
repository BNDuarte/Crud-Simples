using Crud.Domain.Consultas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Crud.Domain.Pacientes
{
    public class Paciente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "o nome pode ter no máximo 50 caracteres")]
        public string Nome { get; set; }
        [Required]
        public string CartaoSus { get; set; }
        public ICollection<Consulta> Consultas { get; set; }
    }
}
