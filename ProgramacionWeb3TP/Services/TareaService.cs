using ProgramacionWeb3TP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Services
{
    public class TareaService
    {
        private TaskieContext ctx = new TaskieContext();
        private CarpetaService _carpetaService = new CarpetaService();

        public Tarea ObtenerTareaPorId(int id)
        {
            Tarea tarea = (from t in ctx.Tarea
                               where t.IdTarea == id
                               select t)
                               .FirstOrDefault();

            return tarea;
        }

        public Tarea CrearTarea(Tarea dataTarea, Usuario usuarioActual)
        {
            Tarea nuevaTarea = new Tarea {
                IdCarpeta = dataTarea.IdCarpeta,
                IdUsuario = usuarioActual.IdUsuario,
                Nombre = dataTarea.Nombre,
                Descripcion = dataTarea.Descripcion,
                EstimadoHoras = dataTarea.EstimadoHoras,
                FechaFin = dataTarea.FechaFin,
                Completada = dataTarea.Completada,
                Prioridad = dataTarea.Prioridad,
                FechaCreacion = DateTime.Now
            };

            usuarioActual.Tarea.Add(nuevaTarea);
            ctx.Tarea.Add(nuevaTarea);
            ctx.SaveChanges();

            return nuevaTarea;
        }

        public List<Tarea> ObtenerTareasPorUsuario(int usuarioId)
        {
            var Tareas = (from t in ctx.Tarea
                            where t.IdUsuario == usuarioId
                            select t)
                            .ToList();

            return Tareas;
        }

        public List<Tarea> ObtenerTareasPorCarpeta(int carpetaId)
        {
            var Tareas = (from t in ctx.Tarea
                          where t.IdCarpeta == carpetaId
                          orderby t.Prioridad ascending, t.FechaFin ascending
                          select t)
                          .ToList();

            return Tareas;
        }

        public void EliminarTarea(int tareaId) {
            Tarea tarea = ObtenerTareaPorId(tareaId);
            ctx.Tarea.Remove(tarea);
            ctx.SaveChanges();
        }

        public List<Tarea> listarTareas(int idUsuario) {
            List<Tarea> lista = new List<Tarea>();

            var tareas = (from t in ctx.Tarea
                          where t.IdUsuario == idUsuario
                          orderby t.FechaCreacion descending
                          select t).ToList();

            foreach (Tarea t in tareas) {
                lista.Add(t);
            }

            return lista;
        }

        public List<Tarea> listarTareasIncompletasPorUsuario(int idUsuario) {
            List<Tarea> lista = new List<Tarea>();

            var tareas = (from t in ctx.Tarea
                          where t.IdUsuario == idUsuario &&
                          t.Completada == 0
                          orderby t.Prioridad descending, t.FechaFin ascending
                          select t).ToList();

            foreach (Tarea t in tareas) {
                lista.Add(t);
            }

            return lista;
        }

        public void CrearComentario(int idTarea, string texto)
        {
            Tarea tarea = ObtenerTareaPorId(idTarea);
            ComentarioTarea comentario = new ComentarioTarea
            {
                IdTarea = tarea.IdTarea,
                Tarea = tarea,
                Texto = texto,
                FechaCreacion = DateTime.Now
            };

            ctx.ComentarioTarea.Add(comentario);
            ctx.SaveChanges();
        }

        public void EliminarComentario(int comentarioId)
        {
            ComentarioTarea comentario = ObtenerComentarioPorId(comentarioId);
            ctx.ComentarioTarea.Remove(comentario);
            ctx.SaveChanges();
        }

        public ComentarioTarea ObtenerComentarioPorId(int comentarioId)
        {
            ComentarioTarea comentario = (from c in ctx.ComentarioTarea
                                          where c.IdComentarioTarea == comentarioId
                                          select c).FirstOrDefault();
            return comentario;
        }

        public void CrearAdjunto(int idTarea, string path)
        {
            Tarea tarea = ObtenerTareaPorId(idTarea);
            ArchivoTarea archivo = new ArchivoTarea
            {
                RutaArchivo = path,
                FechaCreacion = DateTime.Now,
                IdTarea = idTarea,
                Tarea = tarea
            };

            ctx.ArchivoTarea.Add(archivo);
            ctx.SaveChanges();
        }

        public void EliminarAdjunto(int adjuntoId)
        {
            ArchivoTarea archivo = ObtenerArchivoPorId(adjuntoId);
            ctx.ArchivoTarea.Remove(archivo);
            ctx.SaveChanges();
        }

        public ArchivoTarea ObtenerArchivoPorId(int adjuntoId)
        {
            ArchivoTarea adjunto = ctx.ArchivoTarea.Where(a => a.IdArchivoTarea == adjuntoId).FirstOrDefault();

            return adjunto;
        }
    }
}