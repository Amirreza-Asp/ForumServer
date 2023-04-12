using Forum.Domain.Entities.Logs;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Forum.Persistence
{
    public class LogDbContext
    {
        protected MongoClient Client { get; }

        public LogDbContext(IConfiguration configuration)
        {
            Client = new MongoClient(configuration.GetValue<String>("MongoSettings:ConnectionString"));
            var database = Client.GetDatabase(configuration.GetValue<String>("MongoSettings:DatabaseName"));

            SeriLog = database.GetCollection<LogModel>(configuration.GetValue<String>("MongoSettings:CollectionName"));
        }

        public IMongoCollection<LogModel> SeriLog { get; }
    }
}
