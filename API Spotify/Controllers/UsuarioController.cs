using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Spotify.Context;
using API_Spotify.DTOs;
using API_Spotify.Models;
using Microsoft.AspNetCore.Mvc;


namespace API_Spotify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly MusicaContext _context;

        public UsuarioController(MusicaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario is null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PerfilDTO usuarioDTO)
        {
            if (usuarioDTO.FotoPerfil == null || usuarioDTO.FotoPerfil.Length == 0)
            {
                return BadRequest("Foto de perfil não foi enviada.");
            }

            using var memoryStreamFoto = new MemoryStream();
            await usuarioDTO.FotoPerfil.CopyToAsync(memoryStreamFoto);
            var fotobase = Convert.ToBase64String(memoryStreamFoto.ToArray());

            var novoUsuario = new Usuario(
                nome: usuarioDTO.Nome,
                id: 0,
                email: usuarioDTO.Email,
                senha: usuarioDTO.Senha,
                musicasCurtidas: usuarioDTO.MusicasCurtidas,
                fotoPerfil: fotobase
            );

            _context.Add(novoUsuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetId), new
            {
                id = novoUsuario.Id
            }, novoUsuario);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuario)
        {
            var userBanco = _context.Usuarios.Find(id);
            if (userBanco is null) return NotFound("Nenhum usuário encontrado.");

            userBanco.Nome = usuario.Nome;
            userBanco.Email = usuario.Email;
            userBanco.Senha = usuario.Senha;
            userBanco.MusicasCurtidas = usuario.MusicasCurtidas;
            userBanco.FotoPerfil = usuario.FotoPerfil;

            _context.Usuarios.Update(userBanco);
            _context.SaveChanges();

            return Ok(userBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var userBanco = _context.Usuarios.Find(id);
            if (userBanco is null) return NotFound("Usuário não encontrado.");

            _context.Usuarios.Remove(userBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}