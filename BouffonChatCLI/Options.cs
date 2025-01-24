using CommandLine;

namespace BouffonChatCLI
{
    public class Options
    {
        [Option('s', "server", Required = true, HelpText = "Specifies the MongoDB server")]
        public string Server { get; set; } = "";

        [Option('d', "db", Required = true, HelpText = "Specifies MongoDB database containing chat data")]
        public string Database { get; set; } = "";

        [Option('u', "user", HelpText = "Specifies MongoDB database user name")]
        public string User { get; set; } = "";

        [Option('p', "password", HelpText = "Specifies MongoDB database password for user name ")]
        public string Password { get; set; } = "";
    }
}
