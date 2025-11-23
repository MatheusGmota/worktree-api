using JobMatching.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace JobMatching.Doc.Samples
{
    public class JobResponseDtoExample : IExamplesProvider<JobResponseDto>
    {
        public JobResponseDto GetExamples()
        {
            return new JobResponseDto(
                Id: 1,
                Title: "Desenvolvedor Full Stack",
                Description: "Responsável pelo desenvolvimento de aplicações web utilizando tecnologias front-end e back-end.",
                Company: "Tech Solutions",
                Location: "São Paulo, SP",
                Salary: "R$ 8.000 - R$ 12.000",
                Type: "CLT",
                Category: "Desenvolvimento de Software"
            );
        }
    }

    public class JobRequestDtoExample : IExamplesProvider<JobRequestDto>
    {
        public JobRequestDto GetExamples()
        {
            return new JobRequestDto(
                Title: "Desenvolvedor Full Stack",
                Description: "Responsável pelo desenvolvimento de aplicações web utilizando tecnologias front-end e back-end.",
                Company: "Tech Solutions",
                Location: "São Paulo, SP",
                Salary: "R$ 8.000 - R$ 12.000",
                Type: "CLT",
                Category: "Desenvolvimento de Software"
            );
        }
    }
}
