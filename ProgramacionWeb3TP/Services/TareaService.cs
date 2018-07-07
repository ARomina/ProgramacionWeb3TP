using ProgramacionWeb3TP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /*public List<Tarea> ObtenerTareasPorUsuario(int usuarioId)
        {
            var Tareas = (from t in ctx.Tarea
                            where t.IdUsuario == usuarioId
                            select t)
                            .ToList();

            return Tareas;
        }*/

        public List<Tarea> ObtenerTareasPorCarpeta(int carpetaId)
        {
            var Tareas = (from t in ctx.Tarea
                          where t.IdCarpeta == carpetaId
                          orderby t.Prioridad ascending, t.FechaFin ascending
                          select t)
                          .ToList();

            return Tareas;
        }

        public void eliminarTarea(int tareaId) {
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

        public async Task<List<Tarea>> listarTareasAsync(int idUsuario) {
            List<Tarea> lista = new List<Tarea>();
            return lista = await Task.Run(() => this.listarTareas(idUsuario));
        }

        public async Task<List<Tarea>> listarTareasIncompletasPorUsuarioAsync(int idUsuario) {
            List<Tarea> lista = new List<Tarea>();
            return lista = await Task.Run(() => this.listarTareasIncompletasPorUsuario(idUsuario));
        }

        public async Task<List<Tarea>> listarTareasCompletasPorUsuarioAsync(int idUsuario) {
            List<Tarea> lista = new List<Tarea>();
            return lista = await Task.Run(() => this.listarTareasCompletasPorUsuario(idUsuario));
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

        public List<Tarea> listarTareasCompletasPorUsuario(int idUsuario) {
            List<Tarea> lista = new List<Tarea>();

            var tareas = (from t in ctx.Tarea
                          where t.IdUsuario == idUsuario &&
                          t.Completada == 1
                          orderby t.Prioridad descending, t.FechaFin ascending
                          select t).ToList();

            foreach (Tarea t in tareas) {
                lista.Add(t);
            }

            return lista;
        }

        public void completarTarea(int tareaId) {
            Tarea tarea = ObtenerTareaPorId(tareaId);
            tarea.Completada = 1;
            ctx.SaveChanges();
        }

        public void CrearComentario(int idTarea, string texto) {
            Tarea tarea = ObtenerTareaPorId(idTarea);
            ComentarioTarea comentario = new ComentarioTarea {
                IdTarea = tarea.IdTarea,
                Tarea = tarea,
                Texto = texto,
                FechaCreacion = DateTime.Now
            };

            ctx.ComentarioTarea.Add(comentario);
            ctx.SaveChanges();
        }
    }
}