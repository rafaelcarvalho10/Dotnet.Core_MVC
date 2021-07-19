using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shop_Correto.Models;

namespace Shop_Correto
{
  public class Lancamento
  {
    [Key]
    [Column("id")] //nome da coluna que eu quero
    public int? Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string cod_lancamento { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    // [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Data_Lancamento { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public float Qtd_pessoas { get; set; }

    public float Vl_oferta { get; set; }

    public float Qtd_dizimistas { get; set; }

    public float Vl_total_dizimos { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int igrejaId { get; set; }

    public Igreja Igreja { get; set; }

  }
}