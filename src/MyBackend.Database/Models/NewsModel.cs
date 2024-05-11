using LinqToDB;
using LinqToDB.Mapping;

namespace MyBackend.Database.Models;

/// <summary>
/// Provides news item model.
/// </summary>
[Table(Schema = DbConstants.Schema, Name = DbConstants.News)]
public sealed class NewsModel
{
    /// <summary>
    /// News identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull, Identity]
    public int Id { get; set; }

    /// <summary>
    /// News datetime.
    /// </summary>
    [Column(DataType = DataType.DateTimeOffset), NotNull]
    public DateTimeOffset DateTime { get; set; }

    /// <summary>
    /// News text.
    /// </summary>
    [Column(DataType = DataType.NVarChar), NotNull]
    public string? Text { get; set; }
}
