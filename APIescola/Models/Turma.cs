using System.ComponentModel.DataAnnotations;

namespace APIescola.Models
{
    public class Turma
    {
        public Guid TurmaId { get; set; }
        public Guid CursoId { get; set; }
        public Curso? Curso { get; set; }
        [Required(ErrorMessage = "O Campo Nome é Obrigatório")]
        [MaxLength(250, ErrorMessage = "O Nome deve ter no máximo 250 caracteres")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O Campo Nome é Obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "A Data Informada não é Válida")]
        public DateOnly DataInicio { get; set; }
        [Required(ErrorMessage = "O Campo Nome é Obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "A Data Informada não é Válida")]
        public DateOnly DataFim { get; set; }
        [Required(ErrorMessage = "O Campo Nome é Obrigatório")]
        [MaxLength(10, ErrorMessage = "O Nome deve ter no máximo 10 caracteres")]
        public string Sigla { get; set; }
    }
}
