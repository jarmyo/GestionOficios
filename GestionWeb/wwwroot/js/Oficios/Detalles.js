////function Marcar(ido, accion) {
////    //Llenar de información
////    if (accion == 2) {
////        var myModal = new bootstrap.Modal(document.getElementById('modalTurnar'));
////        myModal.show();
////    }
////}

function turnarOficio(oficio, user) {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('modalTurnar'))
    myModal.hide();

    fetch('/Oficios/TurnarOficio?id=' + oficio + "&user=" + user).then(
        function (result) {
            return result.text();
        }).then(
            function (nombreUsuario) {
                console.log(nombreUsuario);
                window.location.reload(true);

            }
        );
}

function AnexarNota(event, idestado) {
    if (event.key == "Enter") {

        //Enviar con fetch
        var t = document.getElementById("TextoNotasDeEstado-" + idestado);
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