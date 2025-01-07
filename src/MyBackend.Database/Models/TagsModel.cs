using LinqToDB;
using LinqToDB.Mapping;

namespace MyBackend.Database.Models;

[Table(Schema = DbConstants.Schema, Name = DbConstants.Tags)]
public sealed class TagsModel
{
    /// <summary>
    /// Tag identifier.
    /// </summary>
    [PrimaryKey, Column(DataType = DataType.Int32), NotNull, Identity]
    public int Id { get; set; }

    /// <summary>
    /// Tag value.
    /// </summary>
    [Column(DataType = DataType.NVarChar), NotNull]
    public string Value { get; set; } = "";
}
