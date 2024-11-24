using Dapper;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using Microsoft.Data.SqlClient;
using Rebus.Bus;
using Rebus.Config.Outbox;
using Rebus.Transport;


namespace DormitoryManagementSystem.Infrastructure.Common.Persistence;

public class DBConnection : IDisposable
{
    private SqlConnection connection;
    private bool connectionOpened;
    private SqlTransaction? transaction;
    private RebusTransactionScope? rebusScope;
    private bool transactionStarted => transaction != null;
    private IDomainEventPublisher domainEventPublisher;

    public DBConnection(string connectionstring, IDomainEventPublisher domainEventPublisher)
    {
        connection = new SqlConnection(connectionstring);
        this.domainEventPublisher = domainEventPublisher;
    }

    public async Task OpenConnectionAsync()
    {
        if (connectionOpened)
            return;

        await connection.OpenAsync();
        connectionOpened = true;
    }

    public async Task BeginTransactionAsync()
    {
        if (transactionStarted)
            return;

        if (!connectionOpened)
        {
            await connection.OpenAsync();
            connectionOpened = true;
        }

        transaction = connection.BeginTransaction();
        rebusScope = new();
        rebusScope.UseOutbox(transaction.Connection, transaction);
    }

    public async Task CommitTransactionAndDisposeAsync(DomainEventRaiser entity)
    {
        ThrowIfTransactionNotStarted();
        await domainEventPublisher.PublishEvents(entity.DomainEvents);
        await rebusScope!.CompleteAsync();
        await transaction!.CommitAsync();
        await CloseTransactionAsync();
    }

    public async Task RollbackTransactionAndDisposeAsync()
    {
        ThrowIfTransactionNotStarted();
        await transaction!.RollbackAsync();
        await CloseTransactionAsync();
    }
    public async Task<IEnumerable<dynamic>> QueryAsync(string query, object? parameters = null) =>
        await QueryAsync<dynamic>(query, parameters);

    public async Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null)
    {
        ThrowIfConnectionNotOpened();
        return await connection.QueryAsync<T>(query, parameters);
    }

    public async Task<dynamic?> QueryFirstOrDefaultAsync(string query, object? parameters = null)
    {
        return await QueryFirstOrDefaultAsync<dynamic>(query, parameters);
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string query, object? parameters = null)
    {
        ThrowIfConnectionNotOpened();
        return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
    }

    public async Task ExecuteAsync(string query, object? parameters = null)
    {
        ThrowIfTransactionNotStarted();
        await connection.ExecuteAsync(query, parameters, transaction);
    }

    public void Dispose()
    {
        if (transactionStarted)
        {
            rebusScope!.Dispose();
            transaction!.Dispose();
        }

        connection.Dispose();
    }

    private void ThrowIfConnectionNotOpened()
    {
        if (!connectionOpened)
            throw new InvalidOperationException("Connection is not opened");
    }

    private void ThrowIfTransactionNotStarted()
    {
        if (!transactionStarted)
            throw new InvalidOperationException("Transaction is not started");
    }

    private async Task CloseTransactionAsync()
    {
        if (!transactionStarted)
            return;

        await transaction!.DisposeAsync();
        rebusScope!.Dispose();
        transaction = null;
    }
}
