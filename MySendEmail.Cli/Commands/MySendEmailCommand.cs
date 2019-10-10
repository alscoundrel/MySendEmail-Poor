using McMaster.Extensions.CommandLineUtils;

namespace MySendEmail.Cli.Commands
{
    [Command("send")]
    public class MySendEmail: CommandBase
    {
        [Option("-t <to>")]
         // [Required] 
         public string To {get; set;}

        [Option("-n <toname>")]
         // [Required] 
         public string ToName {get; set;}

        [Option("-s <subject>")]
         // [Required] 
         public string Subject {get; set;}

        protected override int OnExecute(CommandLineApplication app)
        {

            app.ShowHelp();
            return 1;
        }
    }
}