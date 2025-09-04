namespace TimeToRESTFromTodo.Contracts
{
    public record TaskPatchDTO(string? Title, string? Description, bool? IsCompleted);
}
