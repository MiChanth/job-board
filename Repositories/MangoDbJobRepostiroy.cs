using System;
using System.Collections.Generic;
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

        public void CreateJob(Job job)
        {
            jobsCollection.InsertOne(job);
        }

        public void DeleteJob(Guid id)
        {
            var filter = filterBuilder.Eq(job => job.Id, id);
            jobsCollection.DeleteOne(filter);
        }

        public Job GetJob(Guid id)
        {
            var filter = filterBuilder.Eq(job => job.Id, id);
            return jobsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Job> GetJobs()
        {
            return jobsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateJob(Job job)
        {
             var filter = filterBuilder.Eq(existingJob => existingJob.Id, job.Id);
             jobsCollection.ReplaceOne(filter, job);
        }
    }
}