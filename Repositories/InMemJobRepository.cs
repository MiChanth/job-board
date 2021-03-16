using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobBoardApi.Models;

namespace JobBoardApi.Repositories
{

    public class InMemJobRepository : IJobRepository
    {
        private readonly List<Job> jobs = new()
        {
            new Job { Id = Guid.NewGuid(), Name = "Frontend", Description = "Join an amazing team", Country = "fr" },
            new Job { Id = Guid.NewGuid(), Name = "Backend", Description = "Join an amazing team", Country = "fr" },
            new Job { Id = Guid.NewGuid(), Name = "FullStack", Description = "Join an amazing ESN", Country = "fr" }
        };

        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            return await Task.FromResult(jobs);
        }

        public async  Task<Job> GetJobAsync(Guid id)
        {
            var job = jobs.Where(job => job.Id == id).SingleOrDefault();
            return await Task.FromResult(job);
        }

        public async Task CreateJobAsync(Job job)
        {
            jobs.Add(job);
            await Task.CompletedTask;
        }

        public async Task UpdateJobAsync(Job job)
        {
            var index = jobs.FindIndex(existingJob => existingJob.Id == job.Id);
            jobs[index] = job;
            await Task.CompletedTask;
        }

        public async Task DeleteJobAsync(Guid id)
        {
            var index = jobs.FindIndex(existingJob => existingJob.Id == id);
            jobs.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}