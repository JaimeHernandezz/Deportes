using Deportes.COMMON.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Deportes.COMMON.Interfaces
{
    public interface IManejadorTorneos : IManejadorGenerico<Torneos>
    {
		IEnumerable BuscardorDeTorneos (string Deportes, string Fechas);
		int VerificarNumero(string text);
	}
}
