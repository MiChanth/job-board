using JobBoardApi.Dtos;
using JobBoardApi.Models;

namespace JobBoardApi
{
    public static class Extensions{

        public static JobDto AsDto(this Job job)
        {
            return new JobDto
            {
                Id = job.Id,
                Name = job.Name,
                Description = job.Description,
                Country = job.Country
            };
        }
    }
}