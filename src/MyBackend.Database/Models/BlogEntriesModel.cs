using LinqToDB;
using LinqToDB.Mapping;

namespace MyBackend.Database.Models;

[Table(Schema = DbConstants.Schema, Name = DbConstants.BlogEntries)]
public sealed class BlogEntriesModel
{
    /// <summary>
    /// Blog entry identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull, Identity]
    public int Id { get; set; }

    /// <summary>
    /// Blog entry datetime.
    /// </summary>
    [Column(DataType = DataType.DateTimeOffset), NotNull]
    public DateTimeOffset DateTime { get; set; }

    /// <summary>
    /// Blog entry title.
    /// </summary>
    [Column(DataType = DataType.NVarChar), NotNull]
    public string Title { get; set; } = "";

    /// <summary>
    /// Blog entry text.
    /// </summary>
    [Column(DataType = DataType.NVarChar), NotNull]
    public string Text { get; set; } = "";
}
