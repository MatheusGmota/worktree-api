namespace JobMatching.Application.DTOs
{
    public record JobResponseDto(
        int Id,
        string Title,
        string Description,
        string Company,
        string Location,
        string Salary,
        string Type,
        string Category
    );

    public record JobRequestDto(
        string Title,
        string Description,
        string Company,
        string Location,
        string Salary,
        string Type,
        string Category
    );
}
