using ProgramacionWeb3TP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Services.Interfaces {
	public interface UsuarioServiceInterface{
        Usuario ObtenerUsuarioPorId(int id);
        Usuario loguearUsuarioPorEmail(Usuario usuario);
        Usuario buscarUsuarioPorEmail(string usuario);
        Usuario registrarUsuario(Usuario usuario, string pass2);
        Boolean chequearSiMailsCoinciden(string pass1, string pass2);
        Boolean chequearSiExisteEmail(string email);
        Boolean chequearSiEstaActivo(string email);
        Usuario activarRegistroUsuarioExistente(Usuario usuario);
    }
}
