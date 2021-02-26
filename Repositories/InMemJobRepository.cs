using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Job> GetJobs()
        {
            return jobs;
        }

        public Job GetJob(Guid id)
        {
            return jobs.Where(job => job.Id == id).SingleOrDefault();
        }

        public void CreateJob(Job job)
        {
            jobs.Add(job);
        }

        public void UpdateJob(Job job)
        {
            var index = jobs.FindIndex(existingJob => existingJob.Id == job.Id);
            jobs[index] = job;
        }

        public void DeleteJob(Guid id)
        {
            var index = jobs.FindIndex(existingJob => existingJob.Id == id);
            jobs.RemoveAt(index);
        }
    }
}