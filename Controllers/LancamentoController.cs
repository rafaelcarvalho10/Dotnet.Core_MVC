using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Correto.Data;

namespace Shop_Correto.Controllers
{
  [Route("lancamentos")]
  public class LancamentoController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    // [Authorize(Roles = "funcionario")]
    public async Task<ActionResult<List<Lancamento>>> Get(
                                [FromServices] DataContext context)
    {
      var lancamentos = await context
                  .Lancamentos
                  .Include(x => x.Igreja)
                  .AsNoTracking()
                  .ToListAsync();
      return lancamentos;
    }

    [HttpGet]
    [Route("{id:int}")]
    // [Authorize(Roles = "funcionario")]
    public async Task<ActionResult<Lancamento>> GetById(
      int id,
      [FromServices] DataContext context)
    {
      var lancamento = await context
                      .Lancamentos
                      .Include(x => x.Igreja)
                      .AsNoTracking()
                      .FirstOrDefaultAsync(x => x.Id == id);
      return lancamento;
    }

    [HttpGet]  //lancamento/igreja/1   todos os lancamentos de 1 igreja selecionada
    [Route("igrejas/{id:int}")]
    // [Authorize(Roles = "funcionario")]
    public async Task<ActionResult<List<Lancamento>>> GetByIgreja(
      int id,
      [FromServices] DataContext context)
    {
      var lancamento = await context
                  .Lancamentos
                  .Include(x => x.Igreja)
                  .AsNoTracking()
                  .Where(x => id == 0 || x.igrejaId == id)
                  .ToListAsync();
      return lancamento;
    }


    [HttpPost]
    [Route("")]
    // [Authorize(Roles = "funcionario")]
    // [Authorize(Roles = "employee")]
    public async Task<ActionResult<Lancamento>> Post(
      [FromServices] DataContext context,
      [FromBody] Lancamento model
    )
    {
      if (ModelState.IsValid)
      {
        context.Lancamentos.Add(model);
        await context.SaveChangesAsync();
        return Ok(model);
      }
      else
      {
        return BadRequest(ModelState);
      }
    }

    [HttpPut]
    [Route("{id:int}")]
    // [Authorize(Roles = "funcionario")]
    // [Authorize(Roles = "manager")]
    public async Task<ActionResult<List<Lancamento>>> Put(
      int id, [FromBody] Lancamento model,
              [FromServices] DataContext context)
    {
      if (id != model.Id)
      {
        return NotFound(new { message = "Lancamento não encontrado !" });
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        context.Entry<Lancamento>(model).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return Ok(model);
      }
      catch (DbUpdateConcurrencyException)
      {
        return BadRequest(new { message = "Não foi possivel atualziar o lancamento" });
      }
      catch (Exception)
      {
        return BadRequest(new { message = "Não possivel atualizar o lancamento" });
      }
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "funcionario")]
    // [Authorize(Roles = "manager")]
    public async Task<ActionResult<List<Lancamento>>> Delete(
      int id, [FromServices] DataContext context
    )
    {
      var lancamento = await context.Lancamentos.FirstOrDefaultAsync(x => x.Id == id);
      if (lancamento == null)
      {
        return NotFound(new { message = "Lancamento não encontrado" });
      }
      try
      {
        context.Lancamentos.Remove(lancamento);
        await context.SaveChangesAsync();
        return Ok(new { message = "Lancamento removido com sucesso !" });

      }
      catch (Exception)
      {
        return BadRequest(new { message = "Não foi pssivel remover a categoria" });
      }
    }
  }
}