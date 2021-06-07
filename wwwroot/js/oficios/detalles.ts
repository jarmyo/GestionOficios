function turnarOficio(oficio, user) {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('modalTurnar'))
    myModal.hide();

    fetch('/Oficios/TurnarOficio?id=' + oficio + "&user=" + user).then(
        function (result) { return result.text(); }).then(
            function (nombreUsuario) {
                console.log(nombreUsuario);
                window.location.reload(true);
            }
        );
}

function AnexarNota(event, idestado) {
    if (event.key == "Enter") {
        var t = document.getElementById("TextoNotasDeEstado-" + idestado) as HTMLInputElement;
        fetch('/Oficios/AgregarComentario?id=' + idestado + "&text='" + t.value + "'").then(
            function (result) {
                return result.text();
            }).then(
                function (timeStamp) {

                    if (timeStamp === "error") {
                        t.value = "";
                        console.log("error");
                    }
                    else {
                        var d = document.getElementById("NotasDeEstado-" + idestado);
                        d.innerHTML += '<br/><strong>' + timeStamp + '</strong> ' + t.value;
                        t.value = "";
                    }
                }
            );
    }
}

const inputElement = document.getElementById("subeArchivos") as HTMLInputElement;
inputElement.addEventListener("change", handleFiles, false);
function handleFiles() { const fileList = this.files; /* now you can work with the file list */ }