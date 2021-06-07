var idOficioSeleccionado = -999;

function archivarOficio(idcaja) {
    //fecth comando.
    fetch('/Oficios/ArchivarOficio?id=' + idOficioSeleccionado + "&caja=" + idcaja).then(
        function () {
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalArchivar'));
            modal.hide();
            var row = document.getElementById("OficioRow-" + idOficioSeleccionado) as HTMLTableRowElement;
            row.remove();
            idOficioSeleccionado = -999;
        }
    );
}

function AbrirModalArchivero(idoficio) {
    idOficioSeleccionado = idoficio;
    var myModal = new bootstrap.Modal(document.getElementById('modalArchivar'))
    myModal.show();

}