namespace EnterpriseCraft.Template.Shared.Responses;

public record ApiResponse<T>(T? Data, bool Success = true, string? Error = null);
