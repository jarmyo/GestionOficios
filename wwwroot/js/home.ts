if (!window.Notification) {
    console.log('Browser does not support notifications.');
} else {
    // check if permission is already granted
    if (Notification.permission === 'granted') {
        
    } else {
        // request permission from user
        Notification.requestPermission().then(function (p) {
            if (p === 'granted') {
             
            } else {
                console.log('User blocked notifications.');
            }
        }).catch(function (err) {
            console.error(err);
        });
    }
}

function notificar(titulo: string, mensaje: string) {
    var notify = new Notification(titulo, {
        body: mensaje,
        icon: "/favicon.ico"
    });
       
}
