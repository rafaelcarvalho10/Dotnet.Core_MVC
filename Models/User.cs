using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shop_Correto.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(20, ErrorMessage = "este campo deve conter entre 3 a 60 caracteres")]
    [MinLength(3, ErrorMessage = "este campo deve conter entre 3 a 60 caracteres")]

    public string Password { get; set; }

    public string Role { get; set; }

    public List<Igreja_Usuario> igrejaUsuario { get; set; }
  }
}