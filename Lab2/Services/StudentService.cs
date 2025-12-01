using Lab2.dal.Entities;
using Lab2.dal.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Lab2.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<StudentDto> _studentsCollection;

        public StudentService(IOptions<MongoDBSettings> mongoDBSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = database.GetCollection<StudentDto>("Students");
        }

        public async Task<List<StudentDto>> GetAsync() =>
            await _studentsCollection.Find(_ => true).ToListAsync();

        public async Task<StudentDto?> GetByIdAsync(string id) =>
            await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(StudentDto newStudent) =>
            await _studentsCollection.InsertOneAsync(newStudent);

        public async Task UpdateAsync(string id, StudentDto updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(x => x.Id == id, updatedStudent);

        public async Task RemoveAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(x => x.Id == id);
    }
}