namespace GestionWeb;
public enum EstadoOficio : byte
{
    NoEsMio = 0,
    Capturado = 1,
    PendienteRecibir = 2,
    EnMiPoder = 3,
    EnviadoParaConfirmar = 4,
    Contestado = 5,
    Archivado = 7,
    Eliminado = 9
}