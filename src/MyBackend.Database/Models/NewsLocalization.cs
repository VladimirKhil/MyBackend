using LinqToDB;
using LinqToDB.Mapping;

namespace MyBackend.Database.Models;

/// <summary>
/// Provides a news localization.
/// </summary>
[Table(Schema = DbConstants.Schema, Name = DbConstants.NewsLocalization)]
public sealed class NewsLocalization
{
    /// <summary>
    /// News identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull]
    public int NewsId { get; set; }

    /// <summary>
    /// Language identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull]
    public int LanguageId { get; set; }

    /// <summary>
    /// News localized text.
    /// </summary>
    [Column(DataType = DataType.NVarChar), NotNull]
    public string? Text { get; set; }
}
