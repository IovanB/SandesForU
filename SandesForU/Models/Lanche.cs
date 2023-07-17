using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace SandesForU.Models
{
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }


        [StringLength(80, ErrorMessage = "O tamanho maximo e 100")]
        [Required(ErrorMessage = "Obrigatorio o nome. ")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }


        [MinLength(20, ErrorMessage = "O tamanho mininmo e 20")]
        [MaxLength(200, ErrorMessage = "O tamanho maximo e 200")]
        [Required(ErrorMessage = "Obrigatorio o nome da cateogira ")]
        [Display(Name = "Descricao Curta")]
        public string DescricaoCurta { get; set; }

        [MinLength(20, ErrorMessage = "O tamanho mininmo e 20")]
        [MaxLength(200, ErrorMessage = "O tamanho maximo e 200")]
        [Required(ErrorMessage = "Obrigatorio o nome da cateogira ")]
        [Display(Name = "Descrição")]
        public string DescricaoDetalhada { get; set; }

        [Column(TypeName ="decimal(10,2)")]
        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço")]
        [Range(1,999.99, ErrorMessage ="O preço deve ter entre 1 e 999.99")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage = "O tamanho maximo e 200")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O tamanho maximo e 200")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
