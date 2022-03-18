using Accelerator.Contracts.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Accelerator.API.Shared.Repositories.Base
{
	public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntityDTO
	{
        private readonly string DATABASE = Environment.GetEnvironmentVariable("DATABASE_NAME");
		private readonly IMongoClient _mongoClient;
		private readonly IClientSessionHandle _clientSessionHandle;
		private readonly string _collection;

		public BaseRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, string collection)
		{
			(_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);

			if (!_mongoClient.GetDatabase(DATABASE).ListCollectionNames().ToList().Contains(collection))
				_mongoClient.GetDatabase(DATABASE).CreateCollection(collection);
		}

		protected virtual IMongoCollection<T> Collection =>
		_mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collection);

        //base data manipulation operations
		#region insert
        public async Task InsertAsync(T obj) =>
			await Collection.InsertOneAsync(_clientSessionHandle, obj);
		#endregion

		#region update
		public async Task UpdateAsync(T obj) 
		{
			// build lambda function and retrieve id and value and assign them to the lambda
			Expression<Func<T, string>> func = f => f.QID;
			var value = (string)obj.GetType().GetProperty(func.Body.ToString().Split(".")[1]).GetValue(obj, null);
			var filter = Builders<T>.Filter.Eq(func, value);

			if (obj != null)
				await Collection.ReplaceOneAsync(_clientSessionHandle, filter, obj);
		}
		#endregion

		#region delete
		public async Task DeleteAsync(string id) =>
			await Collection.DeleteOneAsync(_clientSessionHandle, f => f.QID == id);
		#endregion
	}
}
