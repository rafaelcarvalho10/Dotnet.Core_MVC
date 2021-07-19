using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Correto.Data;
using Shop_Correto.Models;

//endpoint é a mesma coisa que uma URL
// https://localhost:50001           //padrão local
// http://localhost:50000           //padrão local
// https://meuapp.azurewebsite     //a URL fica assim quando vai para o azure com meu dominio

[Route("IgrejasUsuario")]
public class IgrejaUsuarioController : ControllerBase
{
  [HttpPost]
  [Route("")]
  // [Authorize(Roles = "gerente")]
  public async Task<ActionResult<List<Igreja_Usuario>>> Post(
            [FromBody] Igreja_Usuario model,
            [FromServices] DataContext context
            )
  {
    if (!ModelState.IsValid) //pega a validação que eu fiz la na model para cada campo
    {
      return BadRequest(ModelState);
    }
    try
    {
      context.Igreja_Usuarios.Add(model);
      await context.SaveChangesAsync();
      return Ok(model);
    }
    catch (Exception)
    {
      return BadRequest(new { mensagem = "Não foi possivel criar !" });
    }
  }
}