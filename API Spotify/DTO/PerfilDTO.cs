using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Spotify.DTOs
{
    public class PerfilDTO
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required IFormFile FotoPerfil { get; set; }
    }
}