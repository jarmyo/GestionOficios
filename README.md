# Gestion de Oficios

Sistema para gestión y control de oficios para dependencias de Gobierno.

Este software tiene como finalidad el control de flujo de los oficios recibidos y enviados en una dependencia.

## Deployment

Deberá definir las variables de entorno con las cadenas de conexión de datos y la base con las credenciales de usuario

- No se incluye el schema de la base de datos, use ingeniería inversa sobre las clases de entityframework (code first).
- Se deberán compilar los archivos typescript a javascript.
- Utilizar un certificado para https sobre el servidor para utilizar las funciones de notificación del navegador; configurar nginx, krestel, el firewall para aceptar conexiones seguras.
- Este proyecto está en desarrollo, para uso de sistemas de repos software

## A futuro

Este proyecto está en desarrollo, pero me gustaría implementar (aprender) tecnologías como blockchain para tener certeza y machinelearning para detectar anomalías.
