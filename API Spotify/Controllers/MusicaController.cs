using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API_Spotify.Models;
using API_Spotify.Context;

namespace API_Spotify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicaController : ControllerBase
    {

        private readonly MusicaContext _context;

        public MusicaController(MusicaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var musica = _context.Musicas.Find(id);
            if (musica is null) return NotFound();
            return Ok(musica);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Musica musica)
        {
            _context.Add(musica);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetId), new { id = musica.Id }, musica);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Musica musica)
        {
            var musicaBD = _context.Musicas.Find(id);
            if (musicaBD is null) return NotFound("Música não encontrada.");
            musicaBD.Nome = musica.Nome;
            musicaBD.Duracao = musica.Duracao;
            musicaBD.Artista = musica.Artista;
            musicaBD.Album = musica.Album;

            _context.Musicas.Update(musicaBD);
            _context.SaveChanges();

            return Ok(musicaBD);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var musicaBD = _context.Musicas.Find(id);
            if (musicaBD is null) return NotFound("Música não encontrada.");

            _context.Musicas.Remove(musicaBD);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("BuscarPorAlbum/{album}")]
        public IActionResult BuscarPorAlbum(string album)
        {
            var musicaBD = _context.Musicas.Where(m => m.Album.Contains(album));
            if (musicaBD is null) return NotFound("Álbum não encontrado.");
            return Ok(musicaBD);
        }

        [HttpGet("BuscarPorNome/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            var musicaBD = _context.Musicas.Where(m => m.Nome.Contains(nome));
            if (musicaBD is null) return NotFound("Álbum não encontrado.");
            return Ok(musicaBD);
        }

        [HttpGet("BuscarPorArtista/{artista}")] 
        public IActionResult BuscarPorArtista(string artista)
        {
            var musicaBD = _context.Musicas.Where(m => m.Artista.Contains(artista));
            if (musicaBD is null) return NotFound("Álbum não encontrado.");
            return Ok(musicaBD);
        }
    }
}