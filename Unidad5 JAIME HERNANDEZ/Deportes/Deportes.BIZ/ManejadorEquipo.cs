using Deportes.COMMON.Entidades;
using Deportes.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deportes.BIZ
{
	public class ManejadorEquipo : IManejadorEquipos
	{
		IRepositorio<Equipo> repositorio;
		public ManejadorEquipo(IRepositorio<Equipo> repositorio)
		{
			this.repositorio = repositorio;
		}
		public List<Equipo> Listar => repositorio.Read;

		public bool Agregar(Equipo entidad)
		{
			return repositorio.Create(entidad);
		}

		public IEnumerable BuscarEquipos(string pal)
		{
			return Listar.Where(e => e.NDeporte == pal).ToList();
		}

		public Equipo BuscarPorId(ObjectId id)
		{
			return Listar.Where(e => e.Id == id).SingleOrDefault();
		}

		public int ContadorDeBuscarEquipo(string pal)
		{
			return Listar.Where(e => e.NDeporte == pal).ToList().Count();
		}

		public bool Eliminar(ObjectId id)
		{
			return repositorio.Delete(id);
		}

		public bool Modificar(Equipo entidad)
		{
			return repositorio.Update(entidad);
		}
	}
}
