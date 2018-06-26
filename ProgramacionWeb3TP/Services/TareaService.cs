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

        public Tarea ObtenerTareaPorId(int id)
        {
            Tarea tarea = (from t in ctx.Tarea
                               where t.IdTarea == id
                               select t)
                               .FirstOrDefault();

            return tarea;
        }

        public Tarea CrearTarea(Tarea dataTarea)
        {
            Tarea nuevaTarea = new Tarea
            {
                Nombre = dataTarea.Nombre,
                Descripcion = dataTarea.Descripcion,
                IdCarpeta = dataTarea.IdCarpeta,
                IdUsuario = dataTarea.IdUsuario,
                Completada = 0,
                Prioridad = 1,
                FechaCreacion = DateTime.Now
            };

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
                          select t)
                            .ToList();

            return Tareas;
        }
    }
}