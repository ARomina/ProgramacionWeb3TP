using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Metadata
{
    public class UsuarioMetadata
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }
    }
}