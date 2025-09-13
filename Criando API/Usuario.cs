
namespace Aula_API.Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public Usuario() { }

        public Usuario(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public override string ToString()
        {
            return $"Usuário: ID = {Id}, Nome = {Nome}, Email = {Email}";
        }
    }
}