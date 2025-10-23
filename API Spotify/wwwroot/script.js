const input = document.getElementById("pesquisa");
const lista = document.getElementById("listaMusicas");
const audios = [];

// Função para atrasar a busca (debounce)
function debounce(fn, delay) {
  let timeout;
  return (...args) => {
    clearTimeout(timeout);
    timeout = setTimeout(() => fn(...args), delay);
  };
}

// Função que faz a busca
async function buscarMusicas(busca) {
  if (!busca.trim()) {
    lista.innerHTML = "";
    return;
  }

  try {
    const response = await fetch(
      `http://localhost:5022/api/Musica/Buscar/${encodeURIComponent(busca)}`
    );
    if (!response) throw new Error(`Erro HTTP\nStatus: ${response.status}`);

    const data = await response.json();
    const lista = document.getElementById("listaMusicas");
    lista.innerHTML = "";

    for (let i = 0; i < data.length; i++) {
      const musica = data[i];
      const li = document.createElement("li");
      li.id = "liMusica";
      const p = document.createElement("p");
      p.textContent = `${musica.nome} - ${musica.artista}`;

      const audio = document.createElement("audio");
      audio.controls = true;

      audio.addEventListener("play", () => {
        audios.forEach((a) => {
          if (a !== audio && !a.paused) {
            a.pause();
            a.currentTime = 0;
          }
        });
      });

      audios.push(audio);

      const source = document.createElement("source");
      source.src = `http://localhost:5022/api/Musica/StreamAudio/${musica.id}`;
      source.type = "audio/mpeg";

      const img = document.createElement("img");
      img.src = `http://localhost:5022/api/Musica/StreamCapa/${musica.id}`;

      const btn = document.createElement("button");
      btn.textContent = "Baixar";
      btn.id = "btnDownload";
      btn.addEventListener("click", async () => {
        try {
          const response = await fetch(
            `http://localhost:5022/api/Musica/DownloadAudio/${musica.id}`
          );

          if (!response.ok)
            throw new Error(`Erro HTTP\nStatus: ${response.status}`);

          const blob = await response.blob(); //json n recebe binario (tipo de dado que estou retornando na api)
          const url = window.URL.createObjectURL(blob);

          const a = document.createElement("a");
          a.href = url;
          a.download = `${musica.nome} - ${musica.artista}.mp3`;
          a.click();

          window.URL.revokeObjectURL(url);
        } catch (error) {
          console.error("Erro ao baixar: ", error);
        }
      });

      audio.appendChild(source);
      li.appendChild(img);
      li.appendChild(p);
      li.appendChild(audio);
      li.appendChild(btn);
      lista.appendChild(li);
    }
  } catch (error) {
    console.error("Erro: ", error);
  }
}

// Cria a função de busca com debounce (espera após parar de digitar)
const buscarMusicasDebounced = debounce(buscarMusicas, 500);

// Detecta quando o usuário digita
input.addEventListener("input", (e) => {
  buscarMusicasDebounced(e.target.value);
});
