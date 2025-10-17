const btn = document.getElementById("buscar");

btn.addEventListener("click", async (e) => {
  e.preventDefault();
  let busca = document.getElementById("pesquisa").value;
  if (!busca) alert("Digite algo para buscar.");

  try {
    const response = await fetch(
      `http://localhost:5022/api/Musica/Buscar/${encodeURIComponent(busca)}`
    );
    if (!response) throw new Error(`Erro HTTP\nStatus: ${response.status}`);

    const data = await response.json();
    console.log(data);
    const lista = document.getElementById("listaMusicas");
    lista.innerHTML = "";

    for (let i = 0; i < data.length; i++) {
      const musica = data[i];
      const li = document.createElement("li");
      const h1 = document.createElement("h1");
      h1.textContent = musica.nome;

      const audio = document.createElement("audio");
      audio.controls = true;

      const source = document.createElement("source");
      source.src = `http://localhost:5022/api/Musica/StreamAudio/${musica.id}`;
      source.type = "audio/mpeg";

      const img = document.createElement("img");
      img.src = `http://localhost:5022/api/Musica/StreamCapa/${musica.id}`;

      audio.appendChild(source);
      li.appendChild(h1);
      li.appendChild(img);
      li.appendChild(audio);
      lista.appendChild(li);
    }
  } catch (error) {
    console.error("Erro:", error);
  }
});
