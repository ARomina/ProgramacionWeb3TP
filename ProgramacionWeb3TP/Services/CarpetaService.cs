using ProgramacionWeb3TP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Services
{
    public class CarpetaService
    {
        private static TaskieContext ctx = new TaskieContext();

        public Carpeta ObtenerCarpetaPorId(int? id)
        {
            Carpeta carpeta = ( from u in ctx.Carpeta
                                where u.IdCarpeta == id
                                select u )
                               .FirstOrDefault();

            return carpeta;
        }

        public Carpeta CrearCarpeta(Carpeta dataCarpeta, Usuario usuarioActual)
        {

            Carpeta nuevaCarpeta = new Carpeta
            {
                IdUsuario = dataCarpeta.IdUsuario,
                Nombre = dataCarpeta.Nombre,
                Descripcion = dataCarpeta.Descripcion,
                FechaCreacion = DateTime.Now
            };

            usuarioActual.Carpeta.Add(nuevaCarpeta);
            ctx.Carpeta.Add(nuevaCarpeta);
            ctx.SaveChanges();

            return nuevaCarpeta;
        }

        public List<Carpeta> ObtenerCarpetasPorUsuario(int usuarioId)
        {
            var carpetas = (    from l in ctx.Carpeta
                                where l.IdUsuario == usuarioId
                                orderby l.Nombre ascending
                                select l )
                            .ToList();

            return carpetas;
        }

        public Carpeta EditarCarpeta(Carpeta carpeta)
        {
            Carpeta carpetaActualizada = ObtenerCarpetaPorId(carpeta.IdCarpeta);

            carpetaActualizada.Nombre = carpeta.Nombre;
            carpetaActualizada.Descripcion = carpeta.Descripcion;

            ctx.SaveChanges();

            return carpetaActualizada;
        }

        public void EliminarCarpeta(int carpetaId)
        {
            Carpeta carpeta = ObtenerCarpetaPorId(carpetaId);

            if (carpeta != null)
            {
                ctx.Carpeta.Remove(carpeta);

                ctx.SaveChanges();
            }
        }

        public Carpeta crearCarpetaGeneral(int usuarioId) {
            Carpeta carpetaGeneral = new Carpeta(usuarioId);
            ctx.Carpeta.Add(carpetaGeneral);
            ctx.SaveChanges();
            return carpetaGeneral;
        }

        public int buscarCarpetaGeneralPorUsuarioId(int usuarioId) {
            Carpeta carpeta = (from c in ctx.Carpeta
                               where c.IdUsuario == usuarioId && c.Nombre == "General"
                               select c).FirstOrDefault();
            if (carpeta == null) {
                carpeta = crearCarpetaGeneral(usuarioId);
            }
            return carpeta.IdCarpeta;
        }
    }
}