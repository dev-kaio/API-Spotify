// const btn = document.getElementById("btn");
// const img = document.getElementById("img");
// const nome = document.getElementById("nomeDog");
// let dataVar;

// document.addEventListener("DOMContentLoaded", carregarFoto);
// btn.addEventListener("click", carregarFoto);

// async function carregarFoto() {
//   try {
//     await fetch("https://dog.ceo/api/breeds/image/random")
//       .then((response) => response.json())
//       .then((data) => (dataVar = data));
//     img.src = dataVar.message;
//     let particaoLink = dataVar.message.split("/");
//     nome.textContent = `Raça: ${particaoLink[4]}`;
//   } catch (err) {
//     console.log(err.message);
//   }
// }

const formUsuario = document.getElementById("formUsuario");
const resultado = document.getElementById("resultado");

formUsuario.addEventListener("submit", async (e) => {
  e.preventDefault();
  const nome = document.getElementById("nome").value;
  const email = document.getElementById("email").value;
  try {
    const response = await fetch("http://localhost:5038/api/Usuarios/Adicionar", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ nome, email }),
    });

    if (!response.ok) throw new Error("Erro ao adicionar usuário");

    const usuario = await response.json();
    resultado.textContent = `Usuário adicionado: ${usuario.nome} - ${usuario.email}`;
  } catch (error) {
    console.error("Erro:", error);
  }
});