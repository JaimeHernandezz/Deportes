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
	public class ManejadorTorneos : IManejadorTorneos
	{
		IRepositorio<Torneos> repositorio;
		public ManejadorTorneos(IRepositorio<Torneos> repositorio)
		{
			this.repositorio = repositorio;
		}
		public List<Torneos> Listar => repositorio.Read;

		public bool Agregar(Torneos entidad)
		{
			return repositorio.Create(entidad);
		}

		public IEnumerable BuscardorDeTorneos(string Deportes, string Fechas)
		{
			return Listar.Where(e => e.Deporte == Deportes && e.Fecha == Fechas).ToList();
		}

		public Torneos BuscarPorId(ObjectId id)
		{
			return Listar.Where(e => e.Id == id).SingleOrDefault();
		}

		public bool Eliminar(ObjectId id)
		{
			return repositorio.Delete(id);
		}

		public bool Modificar(Torneos entidad)
		{
			return repositorio.Update(entidad);
		}

		public int VerificarNumero (string text)
		{
			foreach (var item in text)
			{
				if (!(char.IsNumber(item)))
				{
					return 1;
				}
				else
				{
					return 2;
				}
			}
			return 1;
		}
	}
	
}
