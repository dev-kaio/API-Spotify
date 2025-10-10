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

        // [HttpPost]
        // public IActionResult Post([FromBody] Musica musica)
        // {
        //     _context.Add(musica);
        //     _context.SaveChanges();
        //     return CreatedAtAction(nameof(GetId), new { id = musica.Id }, musica);
        // }

        // [HttpPut("{id}")]
        // public IActionResult Put(int id, Musica musica)
        // {
        //     var musicaBD = _context.Musicas.Find(id);
        //     if (musicaBD is null) return NotFound("Música não encontrada.");
        //     musicaBD.Nome = musica.Nome;
        //     musicaBD.Artista = musica.Artista;
        //     musicaBD.Album = musica.Album;

        //     _context.Musicas.Update(musicaBD);
        //     _context.SaveChanges();

        //     return Ok(musicaBD);
        // }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var musicaBD = _context.Musicas.Find(id);
            if (musicaBD is null) return NotFound("Música não encontrada.");

            _context.Musicas.Remove(musicaBD);
            _context.SaveChanges();
            return NoContent();
        }
        //POST de arquivos mp3 e foto

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> Post([FromForm] MusicaUploadDTO musicaDto)
        {
            if (musicaDto.Audio == null || musicaDto.Capa == null) return BadRequest("Áudio e capa são obrigatórios.");

            using var memoryStreamAudio = new MemoryStream();
            await musicaDto.Audio.CopyToAsync(memoryStreamAudio);
            var audiobase = Convert.ToBase64String(memoryStreamAudio.ToArray());

            using var memoryStreamCapa = new MemoryStream();
            await musicaDto.Capa.CopyToAsync(memoryStreamCapa);
            var capabase = Convert.ToBase64String(memoryStreamCapa.ToArray());

            var musica = new Musica(
                id: 0,
                nome: musicaDto.Nome,
                duracao: musicaDto.Duracao,
                artista: musicaDto.Artista,
                album: musicaDto.Album,
                audiobase: audiobase,
                capa: capabase
            );

            _context.Musicas.Add(musica);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetId), new { id = musica.Id }, musica);
        }

        //Alterar né
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] MusicaUploadDTO musicaDto)
        {
            var musicaBD = _context.Musicas.Find(id);
            if (musicaBD == null) return NotFound("Música não encontrada.");

            musicaBD.Nome = musicaDto.Nome;
            musicaBD.Artista = musicaDto.Artista;
            musicaBD.Album = musicaDto.Album;

            if (musicaDto.Audio != null)
            {
                using var msAudio = new MemoryStream();
                await musicaDto.Audio.CopyToAsync(msAudio);
                musicaBD.AudioBase64 = Convert.ToBase64String(msAudio.ToArray());
            }

            if (musicaDto.Capa != null)
            {
                using var msCapa = new MemoryStream();
                await musicaDto.Capa.CopyToAsync(msCapa);
                musicaBD.CapaBase64 = Convert.ToBase64String(msCapa.ToArray());
            }

            _context.Musicas.Update(musicaBD);
            await _context.SaveChangesAsync();

            return Ok(musicaBD);
        }


        //Baixar musga
        [HttpGet("DownloadAudio/{id}")]
        public IActionResult DownloadAudio(int id)
        {
            var musica = _context.Musicas.Find(id);
            if (musica == null) return NotFound();

            var bytes = Convert.FromBase64String(musica.AudioBase64);
            return File(bytes, "audio/mpeg", $"{musica.Nome}.mp3");
        }

        // [HttpGet("BuscarPorAlbum/{album}")]
        // public IActionResult BuscarPorAlbum(string album)
        // {
        //     var musicaBD = _context.Musicas.Where(m => m.Album.Contains(album));
        //     if (musicaBD is null) return NotFound("Álbum não encontrado.");
        //     return Ok(musicaBD);
        // }

        // [HttpGet("BuscarPorNome/{nome}")]
        // public IActionResult BuscarPorNome(string nome)
        // {
        //     var musicaBD = _context.Musicas.Where(m => m.Nome.Contains(nome));
        //     if (musicaBD is null) return NotFound("Álbum não encontrado.");
        //     return Ok(musicaBD);
        // }

        // [HttpGet("BuscarPorArtista/{artista}")]
        // public IActionResult BuscarPorArtista(string artista)
        // {
        //     var musicaBD = _context.Musicas.Where(m => m.Artista.Contains(artista));
        //     if (musicaBD is null) return NotFound("Álbum não encontrado.");
        //     return Ok(musicaBD);
        // }
    }
}