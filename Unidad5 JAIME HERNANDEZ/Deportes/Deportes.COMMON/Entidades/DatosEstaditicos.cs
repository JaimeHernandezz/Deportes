using System;
using System.Collections.Generic;
using System.Text;

namespace Deportes.COMMON.Entidades
{
    public class DatosEstaditicos
    {
		public DatosEstaditicos()
		{
			Puntos = new List<EstadisticosPuntuacion>();
		}

		public List<EstadisticosPuntuacion> Puntos { get; set; }

		public List<EstadisticosPuntuacion> GeneradorDatos(List<EstadisticosDeTorneos> listatorneo, double limiteInferior, double limiteSuperior, double incremento)
		{
			Puntos = new List<EstadisticosPuntuacion>();
			double contador = 1;
			foreach (var item in listatorneo)
			{
				Puntos.Add(new EstadisticosPuntuacion(contador++, item.DatosDeportes));
			}
			return Puntos;
		}
	}
}
