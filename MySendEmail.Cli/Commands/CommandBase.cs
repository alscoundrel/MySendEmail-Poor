using McMaster.Extensions.CommandLineUtils;

namespace MySendEmail.Cli.Commands
{
    [HelpOption("--help")]
    public abstract class CommandBase
    {

        protected virtual int OnExecute(CommandLineApplication app)
        {   
            return 0;
        }
    }
}