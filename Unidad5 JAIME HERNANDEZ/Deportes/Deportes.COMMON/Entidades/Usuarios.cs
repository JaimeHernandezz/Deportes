using System;
using System.Collections.Generic;
using System.Text;

namespace Deportes.COMMON.Entidades
{
   public class Usuarios:Base
    {
		public string NuevoUsuario { get; set; }
		public string Contraseña { get; set; }
		public string ConfirmarContraseña { get; set; }
		public override string ToString()
		{
			return NuevoUsuario;
		}
	}
}
