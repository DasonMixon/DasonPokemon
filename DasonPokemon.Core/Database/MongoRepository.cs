﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Models;
using MongoDB.Extensions.Repository.Extensions;
using System.Linq.Expressions;
using System.Linq;

namespace MongoDB.Extensions.Repository
{
    /// <summary>
    /// A MongoDB based repository of <see cref="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class MongoRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : MongoEntity
    {
        private readonly IMongoContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MongoRepository(IMongoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
            await FindOneAsync(Filter.IdEq(id), null, cancellationToken);


        public virtual async Task<IEnumerable<TEntity?>> GetManyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) =>
            await FindAsync(expression, null, cancellationToken);

        public virtual async Task<IEnumerable<TEntity?>> GetManyAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default) =>
            await FindAsync(filter, null, cancellationToken);

        /// <summary>
        /// Gets all entities in this repository.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await FindAsync(Filter.Empty, null, cancellationToken);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Adds the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            await collection.InsertManyAsync(entities, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Deletes the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            var result = await collection.FindOneAndDeleteAsync(Filter.IdEq(id), cancellationToken: cancellationToken);
            return result?.Id == id;
        }

        /// <summary>
        /// Replaces the specified entity with the same identifier.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The replaced document.</returns>
        public virtual async Task<TEntity> ReplaceAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var collection = await GetCollectionAsync(cancellationToken);
            return await collection.FindOneAndReplaceAsync(
                Filter.IdEq(entity.Id), entity,
                new FindOneAndReplaceOptions<TEntity> { ReturnDocument = ReturnDocument.After, IsUpsert = true },
                cancellationToken);
        }

        // TODO: Need a cleaner way of handling this, anytime we update data from tcg api we have to match based on their id
        public virtual async Task<BulkWriteResult<TEntity>> BulkUpsertAsync(IEnumerable<TEntity> entities, bool useExternalId = false, CancellationToken cancellationToken = default)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (!entities.Any())
                return null;

            var collection = await GetCollectionAsync(cancellationToken);

            var bulkOps = new List<WriteModel<TEntity>>();

            foreach (var entity in entities)
            {
                ReplaceOneModel<TEntity> upsertOne;
                if (useExternalId)
                {
                    upsertOne = new ReplaceOneModel<TEntity>(
                    Builders<TEntity>.Filter.Where(x => x.ExternalId == entity.ExternalId),
                    entity)
                    { IsUpsert = true };
                } else
                {
                    upsertOne = new ReplaceOneModel<TEntity>(
                    Builders<TEntity>.Filter.Where(x => x.Id == entity.Id),
                    entity)
                    { IsUpsert = true };
                }

                bulkOps.Add(upsertOne);
            }

            return await collection.BulkWriteAsync(bulkOps, null, cancellationToken);
        }

        /// <summary>
        /// Updates the entity with the specified key according to the specified update definition.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateDefinition">The update definition.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<TEntity> UpdateAsync(Guid id, UpdateDefinition<TEntity> updateDefinition, FindOneAndUpdateOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            return await collection.FindOneAndUpdateAsync(Filter.IdEq(id), updateDefinition, options, cancellationToken);
        }

        /// <summary>
        /// Finds the entity according to the specified filter definition.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter, FindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            var cursor = await collection.FindAsync(filter, options, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Finds all entities according to the specified filter definition.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<ICollection<TEntity>> FindAsync(FilterDefinition<TEntity> filter, FindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            var cursor = await collection.FindAsync(filter, options, cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the Mongo collection that backs this repository.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<IMongoCollection<TEntity>> GetCollectionAsync(CancellationToken cancellationToken = default) =>
            await _context.GetCollectionAsync<TEntity>(cancellationToken);


        protected static FilterDefinitionBuilder<TEntity> Filter => Builders<TEntity>.Filter;

        protected static SortDefinitionBuilder<TEntity> Sort => Builders<TEntity>.Sort;

        protected static UpdateDefinitionBuilder<TEntity> Update => Builders<TEntity>.Update;

        protected static ProjectionDefinitionBuilder<TEntity> Projection => Builders<TEntity>.Projection;
    }
}
