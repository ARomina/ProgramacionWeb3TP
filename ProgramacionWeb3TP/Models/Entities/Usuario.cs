using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ProgramacionWeb3TP.Models.Metadata;

namespace ProgramacionWeb3TP.Models
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {
        public string NombreCompleto
        {
            get
            {
                return this.Apellido + ", " + this.Nombre;
            }
            set { }
        }
    }
    
}