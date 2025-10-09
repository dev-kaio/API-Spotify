using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Spotify.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Spotify.Context
{
    public class MusicaContext : DbContext
    {
        public MusicaContext(DbContextOptions<MusicaContext> options) : base(options) { }

        public DbSet<Musica> Musicas { get; set; }
    }
}