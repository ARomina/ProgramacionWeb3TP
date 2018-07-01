﻿using ProgramacionWeb3TP.Models;
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
    }
}