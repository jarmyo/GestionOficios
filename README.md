# GestionOficios

Sistema para gestión de oficios para dependencias de Gobierno, permite controlar el flujo de oficios entre dependencias y funcionarios y tener mayor certeza de como va el proceso, tener metricas de desempeño y agilizar un poco la burocracia.

Diseñado sobre .NET Core 5 en linux.


## Usar

Deberá definir las variables de entorno con las cadenas de conexión de datos y la base con las credenciales de usuario

- No se incluye el schema de la base de datos, use ingeniería inversa sobre las clases de entityframework.
- Se deberán compilar los archivo typescript a javascript.
- Utilizar un certificado para https sobre el servidor para utilizar las funciones de notificación del navegador; configurar nginx, krestel, el firewall para aceptar conexiones seguras.
- Este proyecto está en desarrollo, para uso de sistemas de repos software

## A futuro

Este proyecto está en desarrollo, pero me gustaría implementar (aprender) tecnologías como blockchain para tener certeza y machinelearning para detectar anomalías.
