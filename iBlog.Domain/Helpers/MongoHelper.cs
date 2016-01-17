using System.Configuration;
using MongoDB.Driver;

namespace iBlog.Domain.Helpers
{
    public class MongoHelper<T> where T : class
    {
        public IMongoCollection<T> Collection { get; private set; }

        public MongoHelper()
        {
            var connectString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
            var mongoUrl = new MongoUrl(connectString);

            var client = new MongoClient(connectString);

            var db = client.GetDatabase(mongoUrl.DatabaseName);
            Collection = db.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}
