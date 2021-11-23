function turnarOficio(idOficio: string, idUsuario: string) {

    var myModal = bootstrap.Modal.getInstance(document.getElementById('modalTurnar'))
    myModal.hide();

    fetch('/Oficios/TurnarOficio?id=' + idOficio + "&user=" + idUsuario).then(
        function (result) { return result.text(); }).then(
            function () {                
                window.location.reload();
            }
        );
}

function AnexarNota(event, idestado:string) {
    if (event.key == "Enter") {
        var textInput = document.getElementById("TextoNotasDeEstado-" + idestado) as HTMLInputElement;
        fetch('/Oficios/AgregarComentario?id=' + idestado + "&text='" + textInput.value + "'").then(
            function (result) {
                return result.text();
            }).then(
                function (timeStamp) {

                    if (timeStamp === "error") {
                        textInput.value = "";
                        console.log("error");
                    }
                    else {
                        var d = document.getElementById("NotasDeEstado-" + idestado);
                        d.innerHTML += '<br/><strong>' + timeStamp + '</strong> ' + textInput.value;
                        textInput.value = "";
                    }
                }
            );
    }
}

const inputElement = document.getElementById("subeArchivos") as HTMLInputElement;
inputElement.addEventListener("change", handleFiles, false);
function handleFiles() { const fileList = this.files; /* now you can work with the file list */ }