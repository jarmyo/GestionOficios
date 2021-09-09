function AgregarNuevoEmisor(): void {
    var nom = document.getElementById('NombreEmisor') as HTMLInputElement;

    if (nom.value.length > 0) {

        var tipo = document.getElementById('TipoEmisor') as HTMLSelectElement;
        if (tipo.selectedIndex >= 0) {
            fetch('/Oficios/CrearNuevoEmisor?nombre=' + nom.value + "&tipo=" + tipo.value).then(
                function (res) {
                    return res.json();
                }
            ).then(
                function (idOpcionNueva) {
                    if (idOpcionNueva != "error") {
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

function AgregarNuevoTipoOficio(): void {
    var nom = document.getElementById('NombreTipo') as HTMLInputElement;
    if (nom.value.length > 0) {
        fetch('/Oficios/CrearNuevoTipoOficio?nombre=' + nom.value).then(
            function (res) {
                return res.json();
            }
        ).then(
            function (idOpcionNueva) {
                if (idOpcionNueva != "error") {
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

function CalcularDias(): void {    
    var esHabil = (document.getElementById('radioHabil') as HTMLInputElement).checked;
    var num = 1;
    var numString = "";
    var nuevaFecha = new Date();
    if (esHabil) {
        //TODO verificar que no esté vacio                
        numString = (document.getElementById('calculaHabiles') as HTMLInputElement).value;
        console.log("calcula habiles " + numString);
        num = parseInt(numString);
        nuevaFecha = CalcularDiasHabiles(new Date(), num);
    }
    else {
        //TODO verificar que no esté vacio
        numString = (document.getElementById('calculaNaturales') as HTMLInputElement).value;
        console.log("calcula naturales " + numString);
        num = parseInt(numString);
        nuevaFecha.setDate(nuevaFecha.getDate() + num);
    }    
    var textoFecha = (document.getElementById('OficiosTerminoFecha') as HTMLInputElement);
    console.log("result: " + nuevaFecha.toISOString());
    textoFecha.value = nuevaFecha.toISOString().substr(0, 16);

    var myModal = bootstrap.Modal.getInstance(document.getElementById('calcularfecha'))
    myModal.hide();
}

function CalcularDiasHabiles(fechaInicial: Date, diasHabiles: number): Date {

    var agregarDias = (diasHabiles > 0); //Se pueden restar días.
    var EsMasDeUnaSemana = (diasHabiles > 5 || diasHabiles < -5);
    var diaDeLaSemana = fechaInicial.getDay();
    var diasQueSeAgregan = diasHabiles;

    if ((diaDeLaSemana === 0 && agregarDias) || (diaDeLaSemana === 6 && !agregarDias)) {
        diasQueSeAgregan = diasQueSeAgregan + (1 * (agregarDias ? 1 : -1));
    } else if ((diaDeLaSemana === 6 && agregarDias) || (diaDeLaSemana === 0 && !agregarDias)) {
        diasQueSeAgregan = diasQueSeAgregan + (2 * (agregarDias ? 1 : -1));
    }

    if (EsMasDeUnaSemana) {
        diasQueSeAgregan = diasQueSeAgregan + (2 * (Math.floor(diasHabiles / 5)));

        if (diasHabiles % 5 != 0)
            diasQueSeAgregan = diasQueSeAgregan + (2 * (agregarDias ? -1 : 1));
    }
    fechaInicial.setDate(fechaInicial.getDate() + diasQueSeAgregan);
    return fechaInicial;
}