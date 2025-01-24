using BouffonChatCLI.Models;
using CommandLine;
using MongoDB.Driver;

namespace BouffonChatCLI
{
    internal class CommandRun
    {
        public static void Run(string[] args) 
        {
            Console.WriteLine();

            Parser.Default.ParseArguments<AddOptions, ListOptions, ActivateOptions, DeactivateOptions>(args)
                .MapResult(
                (AddOptions opts) => ProcessAdd(opts),
                (ListOptions opts) => ProcessList(opts),
                (ActivateOptions opts) => ProcessActivate(opts),
                (DeactivateOptions opts) => ProcessDeactivate(opts),
                err => 1);

            Console.WriteLine();
        }

        private static int ProcessDeactivate(DeactivateOptions options)
        {
            var dataStore = DataStore.Instance(options);
            return ChangeProfileStatus(dataStore, options.Email, "inactive");
        }

        private static int ChangeProfileStatus(DataStore dataStore, string email, string status)
        {
            try
            {
                var collection = dataStore.Database().GetCollection<Profile>("profiles");
                var filter = Builders<Profile>.Filter.Eq(f => f.Email, email);
                var update = Builders<Profile>.Update.Set(f => f.Status, status);

                var result = collection.UpdateOne(filter, update);

                if (!result.IsAcknowledged)
                {
                    Console.Error.WriteLine($"Update to profile for email {email} was not acknoledged");
                    return 1;
                }

                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine($"Profile for email {email} was change to be {status}");
                    return 0;
                }
                else
                {
                    Console.Error.WriteLine($"Unable to find profile for email {email}");
                    return 1;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return 1;
            }
        }

        private static int ProcessActivate(ActivateOptions options)
        {
            var dataStore = DataStore.Instance(options);
            return ChangeProfileStatus(dataStore, options.Email, "active");
        }

        private static int ProcessList(ListOptions options)
        {
            try
            {
                var dataStore = DataStore.Instance(options);
                var collection = dataStore.Database().GetCollection<Profile>("profiles");
                var filter = Builders<Profile>.Filter.Empty;
                var documents = collection.Find(filter);

                foreach (var document in documents.ToEnumerable())
                {
                    Console.WriteLine($"{document.Email, -30} {document.Status, -10} {document.Name}");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return 1;
            }

            return 0;
        }

        private static int ProcessAdd(AddOptions options)
        {
            try
            {
                var dataStore = DataStore.Instance(options);
                var collection = dataStore.Database().GetCollection<Profile>("profiles");

                var profile = new Profile
                {
                    GoogleId = options.Email,
                    Name = options.Email,
                    Email = options.Email,
                    Status = options.Activate ? "active" : "inactive"
                };

                collection.InsertOne(profile);
                
                Console.WriteLine($"Profile for email {options.Email} added with status {profile.Status}");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return 1;
            }

            return 0;
        }
    }
}
