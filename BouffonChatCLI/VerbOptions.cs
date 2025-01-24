using CommandLine;

namespace BouffonChatCLI
{
    [Verb("add", HelpText = "Adds and whitelists an email address")]
    internal class AddOptions : Options
    {
        [Option('e', "email", Required = true, HelpText = "Email address to whitelist")]
        public string Email { get; set; } = "";

        [Option('a', "activate", HelpText = "Activates newly added profile")]
        public bool Activate { get; set; }
    }

    [Verb("list", HelpText = "List profiles")]
    internal class ListOptions : Options
    {

    }

    [Verb("activate", HelpText = "Activates profile for email address")]
    internal class ActivateOptions : Options
    {
        [Option('e', "email", Required = true, HelpText = "Email address to whitelist")]
        public string Email { get; set; } = "";
    }

    [Verb("deactivate", HelpText = "Deactivates profile for email address")]
    internal class DeactivateOptions : Options
    {
        [Option('e', "email", Required = true, HelpText = "Email address to whitelist")]
        public string Email { get; set; } = "";
    }

}
