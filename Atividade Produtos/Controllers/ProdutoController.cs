using Microsoft.AspNetCore.Mvc;
using Atividade_Produtos.Models;

namespace Atividade_Produtos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static List<Produto> produtos = new();

        [HttpGet]
        public IActionResult Get() => Ok(produtos);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            return produto is null ? NotFound("Produto não encontrado.") : Ok(produto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto novoProduto)
        {
            novoProduto.Id = produtos.Count + 1;
            produtos.Add(novoProduto);
            return CreatedAtAction(nameof(GetById), new { id = novoProduto.Id }, novoProduto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Produto produtoAtualizado)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto is null) return NotFound("Produto não encontrado.");
            produto.Nome = produtoAtualizado.Nome;
            produto.Preco = produtoAtualizado.Preco;
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto is null) return NotFound("Produto não encontrado.");

            produtos.Remove(produto);
            return NoContent();
        }
    }
}