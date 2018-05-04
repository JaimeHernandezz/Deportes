using Deportes.COMMON.Entidades;
using Deportes.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deportes.BIZ
{
	public class ManejadorDeporte : IManejadorDeportes
	{
		IRepositorio<Deporte> repositorio;
		public ManejadorDeporte(IRepositorio<Deporte> repositorio)
		{
			this.repositorio = repositorio;
		}
		public List<Deporte> Listar => repositorio.Read;

		public bool Agregar(Deporte entidad)
		{
			return repositorio.Create(entidad);
		}

		public Deporte BuscarPorId(ObjectId id)
		{
			return Listar.Where(e => e.Id == id).SingleOrDefault();
		}

		public bool Eliminar(ObjectId id)
		{
			return repositorio.Delete(id);
		}

		public bool Modificar(Deporte entidad)
		{
			return repositorio.Update(entidad);
		}
	}
}
