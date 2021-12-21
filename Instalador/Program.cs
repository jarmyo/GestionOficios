using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Instalador
{
    class Program
    {
        readonly static string certFile = "-----BEGIN CERTIFICATE-----\r\nMIIEDzCCAvegAwIBAgIUQgmNx+My82mF9tacoBzm3TQo/9swDQYJKoZIhvcNAQEL\r\nBQAwgZYxCzAJBgNVBAYTAk1YMRAwDgYDVQQIDAdDaGlhcGFzMRIwEAYDVQQHDAlU\r\nYXBhY2h1bGExFzAVBgNVBAoMDlJlcG9zIFNvZnR3YXJlMRgwFgYDVQQLDA9SZXBv\r\ncyBmb3IgU1NQRUExDjAMBgNVBAMMBVJlcG9zMR4wHAYJKoZIhvcNAQkBFg9qdWxp\r\nYW5AcmVwb3MubXgwHhcNMjAwODE4MTUyNzM4WhcNMjIxMTIxMTUyNzM4WjCBljEL\r\nMAkGA1UEBhMCTVgxEDAOBgNVBAgMB0NoaWFwYXMxEjAQBgNVBAcMCVRhcGFjaHVs\r\nYTEXMBUGA1UECgwOUmVwb3MgU29mdHdhcmUxGDAWBgNVBAsMD1JlcG9zIGZvciBT\r\nU1BFQTEOMAwGA1UEAwwFUmVwb3MxHjAcBgkqhkiG9w0BCQEWD2p1bGlhbkByZXBv\r\ncy5teDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBALIBztOXZ023xSiQ\r\naRRZoLqxUOydNiUY6nM3F+VUqAXMuy2IxAWNnevL9j6evBOmQUNptApUMZ5hS8aQ\r\nMESYVHbqPSLKeiKehxFzv2dfh9W12TKIlIaIUo2xm5nPeJLtys6K7ybREG4kSsQD\r\nUd/q9p1hIzsLYKp0uFGf6nFypkmUAqrLdIJu37rHfcZRcKAaxnngiQiXbeQ8LJX0\r\nhrXw6pSWL99kX3g1vk3GbBKCYID6toU3Ml/QTupgdJA+Ght6dp9HQoXY4a4M5IMQ\r\n1LYfX2eKz1jQnoKHKlw0VMZxxMI5IVcz5VYmi5LVDCoSj+8sOMx853WH6U/MPerp\r\n9/Zuz10CAwEAAaNTMFEwHQYDVR0OBBYEFIxzesU24TuPUirdMtvLXAH5zoZqMB8G\r\nA1UdIwQYMBaAFIxzesU24TuPUirdMtvLXAH5zoZqMA8GA1UdEwEB/wQFMAMBAf8w\r\nDQYJKoZIhvcNAQELBQADggEBAG8jS6GonbtqzfvDRulM9CN08DDDyx0bxawuKIm3\r\n2PQbV9Ku9PZyZxDufvjFJFLLlWtWP2zuTXocUrtMx6BbDkq9U7N22u42HUxZqcAl\r\nn/AiogJhRcpNI/aX1bGYkX6esJ7kDvpl3jkZky4Xt7hdfW7Yr83auVeCQ8Dl1V9m\r\nYAU2K67qA+26Klv8wfNTEXh4VNVYzpvwJMCl7yUUwgIBaE3g3S7yYJ4qCmkMfm6F\r\niwH+V5yGBdmwkv6SHkKmHZFUmM4h2zmAqiA7N/8YapxN3tiz94IGBDgiUY+8fUSF\r\n1Sdr8rvw8oHYjL5jhi6g7hwHx6O77/2am9WehtP8nOphvmM=\r\n-----END CERTIFICATE-----";
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Instalando...");
                #region Instala Certificado                
                Console.Write("Instalando certificados de seguridad");
                var bytes = Encoding.ASCII.GetBytes(certFile);
                X509Certificate2 certificate = new X509Certificate2(bytes);
                X509Store store = new X509Store(StoreName.AuthRoot, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);
                store.Close();
                store = new X509Store(StoreName.AuthRoot, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);
                store.Close();
                Console.WriteLine("...OK");
                #endregion
                //TODO: Crear entrada en hosts
                //TODO: Crear acceso directo en el escritorio.
                Console.WriteLine("Instalado!");
            }
            catch (Exception ee)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("...ERROR: " + ee.Message);
            }
            finally
            {
                Console.WriteLine("Presione Q para salir");
            }
            char q;
            do
            {
                q = Console.ReadKey().KeyChar;
            } while (q != 'Q' && q != 'q');
        }
    }
}