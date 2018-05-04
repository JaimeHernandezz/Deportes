﻿using Deportes.COMMON.Entidades;
using Deportes.COMMON.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deportes.DAL
{
	public class RepositorioGenerico<T> : IRepositorio<T> where T : Base
	{
		private MongoClient client;
		private IMongoDatabase db;
		public RepositorioGenerico()
		{
			client = new MongoClient(new MongoUrl("mongodb://JAIMEHDZ:Jaime2323@ds113358.mlab.com:13358/deportesjaime"));
			db = client.GetDatabase("deportesjaime");
		}
		private IMongoCollection<T> Collection()
		{
			return db.GetCollection<T>(typeof(T).Name);
		}

		public List<T> Read => Collection().AsQueryable().ToList();

		public bool Create(T entidad)
		{
			try
			{
				Collection().InsertOne(entidad);
				return true;
			}
			
			catch (Exception)
			{

				return false;
			}
			

		}

		public bool Delete(ObjectId id)
		{
			try
			{
				return Collection().DeleteOne(p => p.Id == id).DeletedCount == 1;
			}
			catch (Exception)
			{
				return false;
			}

		}

		public bool Update(T entidadModificada)
		{
			try
			{
				return Collection().ReplaceOne(p => p.Id ==entidadModificada.Id,entidadModificada).ModifiedCount ==1;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
