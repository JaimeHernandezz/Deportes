using System;
using System.Collections.Generic;
using System.Text;

namespace Deportes.COMMON.Entidades
{
	public class Torneos : Base
	{
		public string Fecha { get; set; }
		public string Deporte { get; set; }
		public string PrimerEquipo { get; set; }
		public string SegundoEquipo { get; set; }
		public int Puntacion1 { get; set; }
		public int puntuacion2 { get; set; }
    }
}
