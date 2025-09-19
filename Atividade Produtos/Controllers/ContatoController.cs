using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atividade_Produtos.Context;
using Atividade_Produtos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atividade_Produtos.Controllers
{
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_context.Contatos);

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);
            return contato == null ? NotFound() : Ok(contato);
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = contato.Id },
                contato
            );
        }

        [HttpGet("obterPorNome/{nome}")]
        public IActionResult ObterPorNome(string nome)
        {
            var contato = _context.Contatos.Where(x => x.Nome.Contains(nome));

            return contato == null ? NotFound() : Ok(contato);
        }
    }
}