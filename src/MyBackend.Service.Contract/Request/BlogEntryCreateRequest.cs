namespace MyBackend.Service.Contract.Request;

public sealed record BlogEntryCreateRequest(DateTimeOffset DateTime, string Title, string Text, int[] Tags);
