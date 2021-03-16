using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobBoardApi.Dtos;
using JobBoardApi.Models;
using JobBoardApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardApi.Controllers
{
    [ApiController]
    [Route("jobs")]

    public class JobsController : ControllerBase 
    {    
        private readonly IJobRepository repository;

        public JobsController(IJobRepository repository)
        {
            this.repository = repository;
        }
        
        // GET /jobs
        [HttpGet]
        public async Task<IEnumerable<JobDto>> GetJobsAsync(){
            var jobs = (await repository.GetJobsAsync())
                        .Select(job => job.AsDto());
            return jobs;
        }

        // Get /jobs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDto>> GetJobAsync(Guid id){
            var job = await repository.GetJobAsync(id);

            if(job is null)
            {
                return NotFound();
            }

            return job.AsDto();
        }

        // POST /jobs
        [HttpPost]
        public async Task<ActionResult<JobDto>> CreateJobAsync(CreateJobDto jobDto){
            Job job = new(){
                Id = Guid.NewGuid(),
                Name = jobDto.Name,
                Description = jobDto.Description,
                Country = jobDto.Country
            };

            await repository.CreateJobAsync(job);

            return CreatedAtAction(nameof(GetJobAsync), new {id = job.Id}, job.AsDto());
        }

        // PUT /jobs/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateJob(Guid id, UpdateJobDto jobDto)
        {
            var existingJob = await repository.GetJobAsync(id);

            if (existingJob is null)
            {
                return NotFound();
            };

            Job updateJob = existingJob with
            {
                Name = jobDto.Name,
                Description = jobDto.Description,
                Country = jobDto.Country
            };

            await repository.UpdateJobAsync(updateJob);

            return NoContent();
        }

        // DELETE /jobs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJob(Guid id){
            var existingJob = await repository.GetJobAsync(id);

            if (existingJob is null)
            {
                return NotFound();
            };

            await repository.DeleteJobAsync(id);
            return NoContent();
        }
    }
}