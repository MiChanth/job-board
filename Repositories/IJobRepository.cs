using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobBoardApi.Models;

namespace JobBoardApi.Repositories
{
    public interface IJobRepository
    {
        Task<Job> GetJobAsync(Guid id);

        Task<IEnumerable<Job>> GetJobsAsync();

        Task CreateJobAsync(Job job);

        Task UpdateJobAsync(Job job);

        Task DeleteJobAsync(Guid id);
    }
}
    
