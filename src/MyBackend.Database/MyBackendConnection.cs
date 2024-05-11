using LinqToDB;
using LinqToDB.Data;
using MyBackend.Database.Models;

namespace MyBackend.Database;

/// <summary>
/// Defines a database context.
/// </summary>
public sealed class MyBackendConnection(DataOptions options) : DataConnection(options)
{
    /// <summary>
    /// Website news.
    /// </summary>
    public ITable<NewsModel> News => this.GetTable<NewsModel>();
}
