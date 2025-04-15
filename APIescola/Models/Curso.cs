using System.ComponentModel.DataAnnotations;

namespace APIescola.Models
{
    public class Curso
    {
        public Guid CursoId { get; set; }
        [Required(ErrorMessage = "O Campo Nome é Obrigatório")]
        [MaxLength(10, ErrorMessage = "O Nome deve ter no máximo 10 caracteres")]
        public string Sigla { get; set; }
        [Required(ErrorMessage = "O Campo Nome é Obrigatório")]
        [MaxLength(250, ErrorMessage = "O Nome deve ter no máximo 250 caracteres")]
        public string descrição { get; set; }
    }
}
