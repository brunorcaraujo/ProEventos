using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.DTO
{
    public class EventoDTO
    {
        [Display(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "tema")]
        [Required(ErrorMessage = "Campo {0} é obrigatório."),
        //MinLength(3, ErrorMessage = "Campo {0} deve ter no mínimo 4 caracteres."),
        //MaxLength(50, ErrorMessage = "Campo {0} deve ter no máximo 50 caracteres.")
        StringLength(50, MinimumLength = 3, ErrorMessage = "Campo {0} deve ter no mínimo 4 e no máximo 50 caracteres.")]
        public string Tema { get; set; }

        [Display(Name = "local")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Local { get; set; }

        [Display(Name = "data")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string DataEvento { get; set; }

        [Display(Name = "qtdPessoas")]
        [Range(1,20, ErrorMessage = "Campo {0} não pode ser menor que 1 ou maior que 20.")]
        public int QtdPessoas { get; set; }

        [Display(Name = "imagemUrl")]
        [RegularExpression(@".*\.(gif|jpe?g|png|bmp)$", ErrorMessage = "Não é uma {0} válida. (gif, jpeg, jpg, png, bmp)")]
        public string ImagemURL { get; set; }

        [Display(Name = "telefone")]
        [Required(ErrorMessage = "Campo {0} é obirgatório.")]
        [Phone(ErrorMessage = "Campo {0} está com número inválido")]
        public string Telefone { get; set; }

        [Display(Name = "e-mail")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Campo {0} precisa ser válido.")]
        public string Email { get; set; }

        [Display(Name = "lotes")]
        public IEnumerable<LoteDTO> Lotes { get; set; }

        [Display(Name = "redesSociais")]
        public IEnumerable<RedeSocialDTO> RedesSociais { get; set; }

        [Display(Name = "palestrantes")]
        public IEnumerable<PalestranteDTO> Palestrantes { get; set; }
    }
}
