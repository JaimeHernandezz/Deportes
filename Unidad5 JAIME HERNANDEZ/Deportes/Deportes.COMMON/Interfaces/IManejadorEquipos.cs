using Deportes.COMMON.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Deportes.COMMON.Interfaces
{
    public interface IManejadorEquipos: IManejadorGenerico<Equipo>
    {
		IEnumerable BuscarEquipos(string pal);
		int ContadorDeBuscarEquipo(string pal);
    }
}
