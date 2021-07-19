

using Microsoft.EntityFrameworkCore;
using Shop_Correto.Models;

namespace Shop_Correto.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Igreja_Usuario> Igreja_Usuarios { get; set; }
    public DbSet<Igreja> Igrejas { get; set; }
    public DbSet<Lancamento> Lancamentos { get; set; }
    public DbSet<User> Users { get; set; }
  }
}