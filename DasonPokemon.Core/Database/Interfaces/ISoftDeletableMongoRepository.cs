﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Extensions.Repository.Models;

namespace MongoDB.Extensions.Repository.Interfaces
{
    /// <summary>
    /// A MongoDB based repository of <see cref="TEntity" /> that supports soft deletion.
    /// Entities that implement soft deletion should probably have an index defined on the <see cref="SoftDeletableMongoEntity.DateDeleted"/> field.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IMongoRepository{TEntity}" />
    public interface ISoftDeletableMongoRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : SoftDeletableMongoEntity
    {
        /// <summary>
        /// Un-deletes the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> UnDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}