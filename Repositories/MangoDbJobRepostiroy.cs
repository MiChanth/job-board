using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobBoardApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JobBoardApi.Repositories
{
    public class MongoDbJobsRepository : IJobRepository
    {
        private readonly IMongoCollection<Job> jobsCollection;
        private const string databaseName = "jobBoardApi";
        private const string collectionName = "jobs";

        private readonly FilterDefinitionBuilder<Job> filterBuilder = Builders<Job>.Filter;

        public MongoDbJobsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            jobsCollection = database.GetCollection<Job>(collectionName);
        }

        public async Task CreateJobAsync(Job job)
        {
            await jobsCollection.InsertOneAsync(job);
        }

        public async Task DeleteJobAsync(Guid id)
        {
            var filter = filterBuilder.Eq(job => job.Id, id);
            await jobsCollection.DeleteOneAsync(filter);
        }

        public async Task<Job> GetJobAsync(Guid id)
        {
            var filter = filterBuilder.Eq(job => job.Id, id);
            return await jobsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            return await jobsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateJobAsync(Job job)
        {
             var filter = filterBuilder.Eq(existingJob => existingJob.Id, job.Id);
             await jobsCollection.ReplaceOneAsync(filter, job);
        }
    }
}