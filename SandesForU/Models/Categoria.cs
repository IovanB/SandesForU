using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SandesForU.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho maximo e 100")]
        [Required(ErrorMessage = "Obrigatorio o nome da cateogira ")]
        [Display(Name = "Nome")]
        public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho maximo e 200")]
        [Required(ErrorMessage = "Obrigatorio a descricao ")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }

    }
}
