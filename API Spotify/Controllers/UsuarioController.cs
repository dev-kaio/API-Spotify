using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Spotify.Context;
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
        public IActionResult Post([FromBody] Usuario novoUsuario)
        {
            _context.Add(novoUsuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetId), new { id = novoUsuario.Id }, novoUsuario);
        }

        [HttpPut("{id}")]
        public IActionResult
    }
}