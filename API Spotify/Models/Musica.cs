using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Spotify.Models
{
    public class Musica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Artista { get; set; }
        public string Album { get; set; }
        public string AudioBase64 { get; set; }
        public string CapaBase64 { get; set; }

        public Musica() { }

        public Musica(int id, string nome, int duracao, string artista, string album, string audiobase, string capa)
        {
            Id = id;
            Nome = nome;
            Artista = artista;
            Album = album;
            AudioBase64 = audiobase;
            CapaBase64 = capa;
        }
    }
}