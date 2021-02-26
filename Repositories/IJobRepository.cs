using System;
using System.Collections.Generic;
using JobBoardApi.Models;

namespace JobBoardApi.Repositories
{
    public interface IJobRepository
    {
        Job GetJob(Guid id);

        IEnumerable<Job> GetJobs();

        void CreateJob(Job job);

        void UpdateJob(Job job);

        void DeleteJob(Guid id);
    }
}
    
