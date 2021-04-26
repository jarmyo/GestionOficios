using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionWeb
{
    public static class Core
    {

        internal static Dictionary<string, SessionData> SessionesActivas = new Dictionary<string, SessionData>();

        public static string ConnectionString;
        public static bool SetSessionData(string mail)
        {

            var localContext = new Data.GestionOficiosContext();            
            foreach (var m in localContext.Usuarios.ToList())
            {
                if (m.Email.ToLower() == mail)
                {
                    if (!SessionesActivas.ContainsKey(m.AspNetId))
                    {
                        var c = new SessionData
                        {                            
                        };
                        
                        c.NombreUsuario = m.Nombre;
                        c.IdDepartamento = m.IdDepartamento;
                        c.IdUsuario = m.Id;                        
                        return true;
                    }                    
                }
            }
            return false;
        }



        internal static SessionData GetSession(string guid)
        {
            if (Core.SessionesActivas.ContainsKey(guid))
            {
                var t = Core.SessionesActivas[guid];
                return t;
            }
            else
            {
                return Core.GetSessionData(guid);
            }
        }

        private static SessionData GetSessionData(string guid)
        {
            if (!SessionesActivas.ContainsKey(guid))
            {
                var localContext = new Data.GestionOficiosContext();
                foreach (var m in localContext.Usuarios.ToList())
                    if (m.AspNetId == guid)
                    {
                        var c = new SessionData
                        {
                            NombreUsuario = m.Nombre,
                            IdDepartamento = m.IdDepartamento,
                            IdUsuario = m.Id
                        };

                        if (!SessionesActivas.ContainsKey(guid))
                        {
                            SessionesActivas.Add(guid, c);
                            return c;
                        }
                        else
                            return SessionesActivas[guid];
                    }
            }

            return SessionesActivas[guid];
        }
    }
}
