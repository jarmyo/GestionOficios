////if (!window.Notification) {
////    console.log('Browser does not support notifications.');
////} else {
////    // check if permission is already granted
////    if (Notification.permission === 'granted') {

////    } else {
////        // request permission from user
////        Notification.requestPermission().then(function (p) {
////            if (p === 'granted') {

////            } else {
////                console.log('User blocked notifications.');
////            }
////        }).catch(function (err) {
////            console.error(err);
////        });
////    }
////}
if ('serviceWorker' in navigator) {
    
    console.log('service worker' );
        console.log('windows loaded');
        navigator.serviceWorker.register("/js/ServiceWorker.js").then((reg) => {
            if (Notification.permission === "granted") {
                console.log('service ok');
                getSubscription(reg);
            } else if (Notification.permission === "denied") {
                console.log('service denied');
            } else {
                console.log('service default ???');
                requestNotificationAccess(reg);
            }
        });
    
}
else {
    console.log('no service worker');
}


function requestNotificationAccess(reg) {
    console.log('request called');
    Notification.requestPermission(function (status) {
        if (status == "granted") {
            getSubscription(reg);
        } else {
        }
    });
}

function fillSubscribeFields(sub) {
    (document.getElementById("endpoint") as HTMLInputElement).value = sub.endpoint;
    (document.getElementById("p256dh") as HTMLInputElement).value = arrayBufferToBase64(sub.getKey("p256dh"));
    (document.getElementById("auth") as HTMLInputElement).value = arrayBufferToBase64(sub.getKey("auth"));
}

function arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}

function getSubscription(reg) {
    console.log('get sub called');
    console.log(reg);
    reg.pushManager.getSubscription().then(function (sub) {
        console.log("there");
        console.log(sub);

        if (sub === null) {
            console.log('sub is null');
            reg.pushManager.subscribe({
                userVisibleOnly: true,
                applicationServerKey: "BPmsxL_gmC_S648beSlZy3aVCEFa6_RtwPcUv7FEz8z74oVNoynJAtWfxrOK4EFHB6ywofnZJ8JG5_5mSzODieA"
            }).then(function (sub) {
                fillSubscribeFields(sub);
            }).catch(function (e) {
                console.error("Unable to subscribe to push", e);
            });
        } else {         
            fillSubscribeFields(sub);
        }
    });
}

function Notificar(titulo: string, mensaje: string) {
    var notify = new Notification(titulo, {
        body: mensaje,
        icon: "/favicon.ico"
    });

}