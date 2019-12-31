using Microsoft.Extensions.Options;
using MongoDbGenericRepository;

namespace Api.LeaderBoard.Service.Database
{
    public abstract class MongoRepositoryBase : BaseMongoRepository
    {
        public MongoRepositoryBase(
                                   string connectionString,  string database
                                ) : base(connectionString, database)
        {
        }
    }
}
