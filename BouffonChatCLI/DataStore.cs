using MongoDB.Driver;

namespace BouffonChatCLI
{
    internal class DataStore
    {
        private static DataStore? _instance;

        private string _connectionString;
        private string _database;
        private IMongoDatabase? _db = null;

        public DataStore(string connectionString, string database) 
        {
            _connectionString = connectionString;
            _database = database;
        }

        public static DataStore Instance()
        {
            if (_instance is null)
            {
                throw new InvalidOperationException("Data store has not been initialized");
            }

            return _instance;
        }

        public static DataStore Instance(Options options)
        {
            string connectionString;

            if (string.IsNullOrEmpty(options.User))
            {
                connectionString = $"mongodb://{options.Server}";
            }
            else
            {
                connectionString = $"mongodb+srv://{options.User}:{options.Password}@{options.Server}/?retryWrites=true&w=majority";
            }

            return new(connectionString, options.Database);
        }

        public IMongoDatabase Database()
        {
            if (_db is null)
            {
                var client = new MongoClient(_connectionString);
                _db = client.GetDatabase(_database);
            }

            return _db;
        }
    }
}
