

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

[Route("igrejas")]
public class IgrejaController : ControllerBase
{
  // http://localhost:50000/igrejas 
  //todas as igrejas
  [HttpGet]
  [Route("")]
  // [Authorize]
  [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
  public async Task<ActionResult<List<Igreja>>> Get(
    [FromServices] DataContext context
    // AuthenticatedUser _user
  )
  {
    //  var login = HttpContext.User.Claims
    //     .where(u => u.Type == JwtRegisteredClaimNames.Sub)
    //     .Select(u => u.Value)
    //     .FirstOrDefaultAsync();

    var igrejas = await context
        .Igrejas
        .AsNoTracking()
        .ToListAsync();
    return Ok(igrejas);
  }

  //somente 1 igreja selecionada
  [HttpGet]
  [Route("{id:int}")]
  [AllowAnonymous]
  public async Task<ActionResult<Igreja>> GetById(
    int id,
    [FromServices] DataContext context)
  {
    var igreja = await context
                  .Igrejas
                  .AsNoTracking()
                  .FirstOrDefaultAsync(x => x.Id == id);
    return Ok(igreja);
  }

  //criar
  [HttpPost]
  [Route("")]
  // [Authorize(Roles = "employee")]
  // [Authorize]
  public async Task<ActionResult<List<Igreja>>> Post(
            [FromBody] Igreja model,
            [FromServices] DataContext context
            )
  {







    // var igreja = await context.Igrejas.FirstOrDefaultAsync(x => x.cod_igreja == cod_igreja);
    // if (cod_igreja == igreja.cod_igreja)
    // {
    //   return NotFound(new { message = "Código da igreja já existente !" });
    // }


    if (!ModelState.IsValid) //pega a validação que eu fiz la na model para cada campo
    {
      return BadRequest(ModelState);
    }
    try
    {
      context.Igrejas.Add(model);
      await context.SaveChangesAsync();
      return Ok(model);
    }
    catch (Exception)
    {
      return BadRequest(new { mensagem = "Não foi possivel criar a igreja !" });
    }
  }

  //atualizar
  [HttpPut]
  [Route("{id:int}")]
  // [Authorize(Roles = "manager")]
  public async Task<ActionResult<List<Igreja>>> Put(
               int id,
               [FromBody] Igreja model,
               [FromServices] DataContext context)
  {
    // verifica se o ID informado é o mesmo do modelo
    // if (id != model.Id)
    if (id != model.Id)
    {
      return NotFound(new { message = "Igreja não encontrada !" });
    }
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    try
    {
      context.Entry<Igreja>(model).State = EntityState.Modified;
      await context.SaveChangesAsync();
      return Ok(model);
    }
    catch (DbUpdateConcurrencyException)
    {
      return BadRequest(new { message = "Este registro ja foi atualizado !" });
    }
    catch (Exception)
    {
      return BadRequest(new { message = "Não foi possivel atualizar a igreja" });
    }
  }

  //deletar
  [HttpDelete]
  [Route("{id:int}")]
  // [Authorize(Roles = "manager")]
  public async Task<ActionResult<List<Igreja>>> Delete(
    int id,
          [FromServices] DataContext context
  )
  {
    var igreja = await context.Igrejas.FirstOrDefaultAsync(x => x.Id == id);
    if (igreja == null)
    {
      return NotFound(new { message = "Igreja nao encontrada" });
    }
    try
    {
      context.Igrejas.Remove(igreja);
      await context.SaveChangesAsync();
      return Ok(new { message = "Igreja removida com sucesso" });
    }
    catch (Exception)
    {
      return BadRequest(new { message = "Não foi possivel remover a categoria" });
    }

  }
}