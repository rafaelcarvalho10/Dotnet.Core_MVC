using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shop_Correto.Models
{
  [Table("Igreja_Usuario")]  //nome da tabela que eu quero
  public class Igreja_Usuario
  {
    [Key]
    [Column("id")] //nome da coluna que eu quero
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int igrejaId { get; set; }

    public Igreja igreja { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int userId { get; set; }

    [JsonIgnore]
    public User user { get; set; }
  }
}