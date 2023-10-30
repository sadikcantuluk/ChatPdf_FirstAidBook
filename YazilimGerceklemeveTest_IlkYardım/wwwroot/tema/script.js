function ekleMesaj() {
    const yeniMesaj = document.getElementById("yeni-mesaj").value;
    if (yeniMesaj.trim() !== "") {
        const mesajKutusu = document.getElementById("mesaj-kutusu");
        const yeniDiv = document.createElement("div");
        yeniDiv.classList.add("mesaj");
        yeniDiv.innerHTML = `<p class="gonderen">Gönderenin Mesajı</p>
                            <span class="saat">10:00 AM</span>
                            <p class="metin">${yeniMesaj}</p>`;
        mesajKutusu.appendChild(yeniDiv);
        document.getElementById("yeni-mesaj").value = "";
    }
}
