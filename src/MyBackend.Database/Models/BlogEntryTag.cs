using LinqToDB;
using LinqToDB.Mapping;

namespace MyBackend.Database.Models;

/// <summary>
/// Defines a blog entry - tag relation.
/// </summary>
[Table(Schema = DbConstants.Schema, Name = DbConstants.BlogEntriesTags)]
public sealed class BlogEntryTag
{
    /// <summary>
    /// Entry identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull]
    public int BlogEntryId { get; set; }

    /// <summary>
    /// Tag identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull]
    public int TagId { get; set; }
}
