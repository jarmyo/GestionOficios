function AgregarNuevoEmisor() {
    var nom = document.getElementById('NombreEmisor') as HTMLInputElement;

    if (nom.value.length > 0) {

        var tipo = document.getElementById('TipoEmisor') as HTMLSelectElement;
        if (tipo.selectedIndex >= 0) {
            fetch('/Oficios/CrearNuevoEmisor?nombre=' + nom + "&tipo=" + tipo.value).then(
                function (res) {
                    return res.json();
                }
            ).then(
                function (idOpcionNueva) {
                    if (idOpcionNueva == "error") {
                        var tipos = document.getElementById('Oficios_IdEmisor') as HTMLSelectElement;
                        var newOption = document.createElement("option") as HTMLOptionElement;
                        newOption.text = nom.value;
                        newOption.value = idOpcionNueva;
                        tipos.appendChild(newOption);
                        tipos.selectedIndex = tipos.options.length - 1;

                        nom.value = "";
                        tipo.selectedIndex = 0;
                        var myModal = bootstrap.Modal.getInstance(document.getElementById('agregaremisor'))
                        myModal.hide();
                    }
                    else {
                        alert('error, al crear el nuevo emisor');
                    }
                }
            );
        }
        else {
            alert('Tipo de emisor no seleccionado');
        }

    }
    else {
        alert('Nombre vacio');
    }

}

function AgregarNuevoTipoOficio() {
    var nom = document.getElementById('NombreTipo') as HTMLInputElement;
    if (nom.value.length > 0) {
            fetch('/Oficios/CrearNuevoTipoOficio?nombre=' + nom ).then(
                function (res) {
                    return res.json();
                }
            ).then(
                function (idOpcionNueva) {
                    if (idOpcionNueva == "error") {
                        var tipos = document.getElementById('Oficios_IdTipo') as HTMLSelectElement;
                        var newOption = document.createElement("option") as HTMLOptionElement;
                        newOption.text = nom.value;
                        newOption.value = idOpcionNueva;
                        tipos.appendChild(newOption);
                        tipos.selectedIndex = tipos.options.length - 1;

                        nom.value = "";
                        var myModal = bootstrap.Modal.getInstance(document.getElementById('agregartipo'))
                        myModal.hide();
                    }
                    else {
                        alert('error, al crear el nuevo tipo de ofiico');
                    }
                }
            );
    }
    else {
        alert('Nombre vacio');
    }

}