using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Spotify.Models
{
    public class MusicaUploadDTO
    {
        public required string Nome { get; set; }
        public required int Duracao { get; set; }
        public required string Artista { get; set; }
        public required string Album { get; set; }
        public required IFormFile Audio { get; set; }
        public required IFormFile Capa { get; set; }
    }
}