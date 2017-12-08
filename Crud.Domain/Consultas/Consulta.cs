using Crud.Domain.Medicos;
using Crud.Domain.Pacientes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Crud.Domain.Consultas
{
    public class Consulta
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataConsulta { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
    }
}
