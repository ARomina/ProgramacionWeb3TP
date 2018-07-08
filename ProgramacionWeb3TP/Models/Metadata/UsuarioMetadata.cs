﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Metadata
{
    public class UsuarioMetadata
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(50, ErrorMessage = "Debe tener como máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(50, ErrorMessage = "Debe tener como máximo 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un e-mail válido")]
        [StringLength(20, ErrorMessage = "Debe tener como máximo 20 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Debe tener como máximo 20 caracteres")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{0,20}$", ErrorMessage = "Debe contener al menos 1 mayúscula, 1 minúscula y 1 número")]
        public string Contrasenia { get; set; }
    }
}