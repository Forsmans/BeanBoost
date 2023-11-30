using BeanBoost.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BeanBoost.Data
{
    public class BeanBoostDbContext
    {
        private readonly IMongoDatabase _database;

        public BeanBoostDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Student> Students => _database.GetCollection<Student>("Students");

        public async Task DeleteStudentAsync(Student student)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Id, student.Id);
            await Students.DeleteOneAsync(filter);
        }
    }

}
