using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Correto.Data;
using Shop_Correto.Models;
using Shop_Correto.Services;

namespace Shop_Correto.Controllers
{

  //listar usuarios
  [Route("users")]
  public class UserController : Controller
  {

    [HttpGet]
    [Route("")]
    // [Authorize(Roles = "gerente")]
    public async Task<ActionResult<List<User>>> GetAction([FromServices] DataContext context)
    {
      var users = await context
              .Users
              .AsNoTracking()
              .ToListAsync();
      return users;
    }

    [HttpGet]
    [Route("{id:int}")]
    // [Authorize(Roles = "manager")]
    public async Task<ActionResult<List<User>>> GetById(
            int id,
            [FromServices] DataContext context)
    {
      var users = await context
              .Users
              .AsNoTracking()
              .Include(c => c.igrejaUsuario)
              .FirstOrDefaultAsync(x => x.Id == id);
      return Ok(users);
    }


    //criar usuario
    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    // [Authorize(Roles = "gerente")]
    public async Task<ActionResult<User>> Post(
      [FromServices] DataContext context,
      [FromBody] User model)

    {
      //verifica se os dados são validos
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        //forçz o usuario a ser sempre "gerente"
        // model.Role = "gerente";

        context.Users.Add(model);
        await context.SaveChangesAsync();

        //esconde a senha
        model.Password = "";

        // foreach (var item in model.igrejaUsuario)
        // {
        //   var igrejaUsuario = new Igreja_Usuario()
        //   {
        //     igrejaId = item.igrejaId,
        //     userId = model.Id
        //   };
        //   context.Igreja_Usuarios.Add(igrejaUsuario);
        //   await context.SaveChangesAsync();
        // }
        return Ok(model);
      }
      catch (Exception e)
      {
        return BadRequest(new { message = "Não foi possivel criar o usuario" });
      }
    }


    //atualizar usuario
    [HttpPut]
    [Route("{id:int}")]
    // [Authorize(Roles = "gerente")]
    public async Task<ActionResult<User>> Put(
              [FromServices] DataContext context,
              int id,
              [FromBody] User model)
    {
      //Verifica se os dados são validos
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      //Verifica se o Id informado é o mesmo do modelo
      if (id != model.Id)
      {
        return NotFound(new { message = "Usuario não encontrado" });
      }

      try
      {
        context.Entry(model).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return Ok(model);
      }
      catch (Exception)
      {
        return BadRequest(new { message = "não foi possivel atualizar o usuario" });
      }

    }

    //logar no sistema
    [HttpPost]
    [Route("login")]
    // [Authorize(Roles = "gerente")]
    public async Task<ActionResult<dynamic>> Authenticate(
                  [FromServices] DataContext context,
                  [FromBody] User model)
    {
      var user = await context.Users
                        .AsNoTracking()
                        .Where(x => x.Username == model.Username && x.Password == model.Password)
                        .FirstOrDefaultAsync();
      if (user == null)
      {
        return NotFound(new { message = "Usuario ou senha invalida" });
      }

      var token = TokenService.GenerateToken(user); //gerou o token

      //esconde a senha
      user.Password = "";
      return new
      {
        user = user,
        token = token
      };
    }

    [HttpDelete]
    [Route("{id:int}")]
    // [Authorize(Roles = "gerente")]
    public async Task<ActionResult<List<User>>> Delete(
      int id, [FromServices] DataContext context
    )
    {
      var usuario = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
      if (usuario == null)
      {
        return NotFound(new { message = "Usuario não encontrado" });
      }
      try
      {
        context.Users.Remove(usuario);
        await context.SaveChangesAsync();
        return Ok(new { message = "Usuario removido com sucesso !" });
      }
      catch (Exception)
      {
        return BadRequest(new { message = "Não foi possivel reover a categoria" });
      }
    }


  }
}