

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop_Correto;
using Shop_Correto.Data;
using Shop_Correto.Models;

namespace Backoffice.controllers
{
  [Route("v1")]
  public class HomeController : Controller
  {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
    {
      var employee = new User { Id = 1, Username = "rafael", Password = "rafael", Role = "employee" };
      var manager = new User { Id = 2, Username = "rafael", Password = "rafael", Role = "manage" };
      var igreja = new Igreja { Id = 1, cod_igreja = "1", nome_igreja = "igreja 1" };
      var lancamento = new Lancamento
      {
        Id = 1,
        cod_lancamento = "1",
        Qtd_pessoas = 1,
        // Vl_oferta = 1000,
        // Vl_total_dizimos = 3000,
        igrejaId = 1
      };

      context.Users.Add(employee);
      context.Users.Add(manager);
      context.Igrejas.Add(igreja);
      context.Lancamentos.Add(lancamento);
      await context.SaveChangesAsync();

      return Ok(new { message = "Dados configurados" });
    }
  }
}