using Deportes.COMMON.Entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deportes.COMMON.Interfaces
{
    public interface IRepositorio<T> where T:Base
    {
		bool Create(T entidad);
		List<T> Read { get; }
		bool Update(T entidadModificada);
		bool Delete(ObjectId Id);
	}
}
