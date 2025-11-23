namespace JobMatching.Application.DTOs
{
    public record ApplicationRequestDto(

        int JobId,
        int CandidateId,
        string coverLetter
    );

    public record ApplicationResponseDto(
        int Id,
        string coverLetter,
        JobResponseDto Job,
        UserResponseDto Candidate,
        string Status
    );
}
