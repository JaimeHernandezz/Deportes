using System;
using System.Collections.Generic;
using System.Text;

namespace Deportes.COMMON.Entidades
{
    public class Deporte: Base
    {
		public string TipoDeDeporte { get; set; }
		public override string ToString()
		{
			return TipoDeDeporte;
		}
	}
}
