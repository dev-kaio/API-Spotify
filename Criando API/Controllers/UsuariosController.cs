using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Aula_API.Usuarios;

namespace Aula_API
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        //Crie um endpoint GET /ola que retorne a mensagem: { "mensagem": "Olá, mundo!" }
        // [HttpGet("Ola")]
        // public IActionResult Ola()
        // {
        //     string ola = "Olá Mundo!";

        //     return Ok(new { ola });
        // }

        //Crie um endpoint GET /tabuada/{numero} que retorne a tabuada do número passado na rota.
        // [HttpGet("Tabuada/{num}")]
        // public IActionResult Tabuada(float num)
        // {
        //     List<float> tabuados = new List<float>();
        //     for (int i = 1; i <= 10; i++)
        //     {
        //         float numMult = num * i;
        //         tabuados.Add(numMult);
        //     }

        //     return Ok(new { tabuados });
        // }

        //Criando lista de usuários
        static List<Usuario> usuarios = new List<Usuario>();
        public UsuariosController()
        {
            if (usuarios.Count == 0)
            {
                for (int i = 1; i <= 20; i++)
                {
                    usuarios.Add(new Usuario(
                        i,
                        $"Usuario{i}",
                        $"usuario{i}@exemplo.com"
                    ));
                }
            }
        }
        //Puxar todos Users
        [HttpGet("Listar")]
        public IActionResult Usuarios()
        {
            return Ok(new { usuarios });
        }

        //Puxar user específico
        [HttpGet("{id}")]
        public IActionResult UsuarioEspecifico(int id)
        {
            object retorno = "Usuário não encontrado";
            foreach (Usuario users in usuarios)
            {
                if (users.Id == id)
                {
                    retorno = users;
                }
            }
            return Ok(new { retorno });
        }

        [HttpPost("Adicionar")]
        public IActionResult AdicionarUser([FromBody] Usuario usuario)
        {
            usuario.Id = usuarios.Count + 1;
            usuarios.Add(usuario);
            return Ok(usuario);
        }
    }
}