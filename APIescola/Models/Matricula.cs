using System.ComponentModel.DataAnnotations;

namespace APIescola.Models
{
    public class Matricula
    {
        public Guid MatriculaId { get; set; }
        public Guid AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
        public Guid TurmaId { get; set; }
        public Turma? Turma { get; set; }
        [Required(ErrorMessage = "o campo Data da matricula não pode ser nulo")]
        [DataType(DataType.Date, ErrorMessage = "A Data Informada não é Válida")]
        public DateTime DataMatricula { get; set; }
    }
}
