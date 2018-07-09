using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProgramacionWeb3TP.Utils {
    public static class FileUtility {
        public static string Guardar(HttpPostedFileBase archivoSubido, string nombreSignificativo, string carpeta) {

            //Server.MapPath antepone a un string la ruta fisica donde actualmente esta corriendo la aplicacion (ej. c:\inetpub\misitio\)
            string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpeta;

            //si no exise la carpeta, la creamos
            if (!System.IO.Directory.Exists(pathDestino)) {
                System.IO.Directory.CreateDirectory(pathDestino);
            }
            string fileExtension = nombreSignificativo.Split('.').LastOrDefault();
            string fileName = nombreSignificativo.Split('.').FirstOrDefault();
            string nombreArchivoFinal = GenerarNombreUnico(fileName);
            nombreArchivoFinal = string.Concat(nombreArchivoFinal, Path.GetExtension(archivoSubido.FileName));

            //para guardar en el disco rigido, se guarda con el path absoluto
            archivoSubido.SaveAs(string.Concat(pathDestino, nombreArchivoFinal));
            //retornamos el path relativo desde la raiz del sitio
            return string.Concat(carpeta, nombreArchivoFinal);
        }

        private static string GenerarNombreUnico(string nombreSignificativo) {
            return $"{nombreSignificativo}_{Guid.NewGuid().ToString()}";
        }

        public static string Truncar(this string value, int maxLength) {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}