using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atividade_Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace Atividade_Produtos.Context
{
    public class AgendaContext : DbContext
    {

        public AgendaContext(DbContextOptions<AgendaContext> options)
        : base(options) { }

        public DbSet<Contato> Contatos { get; set; }
    }
}