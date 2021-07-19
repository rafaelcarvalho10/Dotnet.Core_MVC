using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Correto.Models
{
  [Table("Igreja")]  //nome da tabela que eu quero
  public class Igreja
  {
    [Key]
    [Column("id")] //nome da coluna que eu quero
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string cod_igreja { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
    public string nome_igreja { get; set; }
  }
}