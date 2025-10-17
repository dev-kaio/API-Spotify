using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Spotify.DTO
{
    public class MusicaBuscaDTO
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required string Artista { get; set; }
        public required string Album { get; set; }
    }
}