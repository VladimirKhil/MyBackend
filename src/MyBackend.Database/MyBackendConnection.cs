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
    
    /// <summary>
    /// Localized news.
    /// </summary>
    public ITable<NewsLocalization> NewsLocalization => this.GetTable<NewsLocalization>();

    /// <summary>
    /// Blog entries.
    /// </summary>
    public ITable<BlogEntriesModel> BlogEntries => this.GetTable<BlogEntriesModel>();

    /// <summary>
    /// Blog entries localization.
    /// </summary>
    public ITable<BlogEntriesLocalization> BlogEntriesLocalization => this.GetTable<BlogEntriesLocalization>();

    /// <summary>
    /// Blog tags.
    /// </summary>
    public ITable<TagsModel> Tags => this.GetTable<TagsModel>();

    /// <summary>
    /// Blog tags localization.
    /// </summary>
    public ITable<TagsLocalization> TagsLocalization => this.GetTable<TagsLocalization>();

    /// <summary>
    /// Blog entry - tags relation.
    /// </summary>
    public ITable<BlogEntryTag> BlogEntriesTags => this.GetTable<BlogEntryTag>();
}
