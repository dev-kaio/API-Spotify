using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Spotify.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<Musica> MusicasCurtidas { get; set; }
        public string FotoPerfil { get; set; }

        public Usuario() { }

        public Usuario(string nome, int id, string email, string senha, List<Musica> musicasCurtidas, string fotoPerfil)
        {
            Nome = nome;
            Id = id;
            Email = email;
            Senha = senha;
            MusicasCurtidas = musicasCurtidas;
            FotoPerfil = fotoPerfil;
        }
    }
}