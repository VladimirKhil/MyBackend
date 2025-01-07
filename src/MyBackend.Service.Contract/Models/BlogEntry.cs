namespace MyBackend.Service.Contract.Models;

public sealed record BlogEntry(int Id, DateTimeOffset DateTime, string Title, string Text, Tag[] Tags);
