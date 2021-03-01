using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<JobDto> GetJobs(){
            var jobs = repository.GetJobs().Select(job => job.AsDto());
            return jobs;
        }

        // Get /jobs/{id}
        [HttpGet("{id}")]
        public ActionResult<JobDto> GetJob(Guid id){
            var job = repository.GetJob(id);

            if(job is null)
            {
                return NotFound();
            }

            return job.AsDto();
        }

        // POST /jobs
        [HttpPost]
        public ActionResult<JobDto> CreateJob(CreateJobDto jobDto){
            Job job = new(){
                Id = Guid.NewGuid(),
                Name = jobDto.Name,
                Description = jobDto.Description,
                Country = jobDto.Country
            };

            repository.CreateJob(job);

            return CreatedAtAction(nameof(GetJob), new {id = job.Id}, job.AsDto());
        }

        // PUT /jobs/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateJob(Guid id, UpdateJobDto jobDto)
        {
            var existingJob = repository.GetJob(id);

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

            repository.UpdateJob(updateJob);

            return NoContent();
        }

        // DELETE /jobs/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteJob(Guid id){
            var existingJob = repository.GetJob(id);

            if (existingJob is null)
            {
                return NotFound();
            };

            repository.DeleteJob(id);
            return NoContent();
        }
    }
}