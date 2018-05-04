using System;
using System.Collections.Generic;
using System.Text;

namespace Deportes.COMMON.Entidades
{
    public class EstadisticosPuntuacion
    {
		public double X { get; set; }
		public double Y { get; set; }
		public EstadisticosPuntuacion(double x, double y){
			X = x;
			Y = y; ;
		}
		public override string ToString(){
			return string.Format("({0},{1})", X, Y);
		}
	}
}
