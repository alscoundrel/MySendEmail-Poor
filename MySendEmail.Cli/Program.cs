using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MySendEmail.Cli.Commands;
using McMaster.Extensions.CommandLineUtils.Conventions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using Scriban;

namespace MySendEmail.Cli
{
    [Command(Name = "app", Description = "App")]
    [HelpOption]
    class Program
    {
        private readonly ConventionContext _conventioncontext;
        public Program(ConventionContext conventionContext){
            _conventioncontext = conventionContext;
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var serviceProvider = BuildServiceProvider();
            var app = new CommandLineApplication<Program>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(serviceProvider);

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var apiKeyString = configuration.GetValue<string>("SendGrid:apiKey");
            var fromString = configuration.GetValue<string>("SendGrid:from");
            var fromNameString = configuration.GetValue<string>("SendGrid:fromname");
            
            // carrega template
            var srTemplate = new StreamReader("template.html");
            string html = srTemplate.ReadToEnd();

            // endereços...
            var srSendValues = new StreamReader("sendvalues.json");
            string sendValues = srSendValues.ReadToEnd();

            JObject o = JObject.Parse(sendValues);
            var subject = (string)o["subject"];

            //contas de email
            JArray jSendValues = (JArray) o["addresses"];
            foreach (var jSendValue in jSendValues)
            {
                var toEmail = (string)jSendValue["email"];
                var toName  = (string)jSendValue["name"];
                
                // parse a scriban template
                // para o assunto
                var template = Template.Parse(subject);
                var resultSubject = template.Render(new {name = toName});

                // para o corpo
                //string txt = "Hello {{name}}!";
                template = Template.Parse(html);
                var resultHTML = template.Render(new {name = toName});

                var singleEmail = new SingleEmail(
                    new EmailAddress(fromString, fromNameString),
                    resultSubject,
                    new EmailAddress(toEmail, toName),
                    "and easy to do anywhere, even with C#",
                    resultHTML
                );
                await EnviaEmail(singleEmail.ConstroiMensagem());
            }

            //return app.Execute(args);
        }

        private static IServiceProvider BuildServiceProvider() {
            var serviceCollection = new ServiceCollection();
                // .AddSingleton<IMyService, MyServiceImplementation>()
            // Configuration
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            configurationBuilder.AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            // Logging
            serviceCollection.AddLogging();
            
            serviceCollection.AddSingleton<IConsole>(PhysicalConsole.Singleton);
            return serviceCollection.BuildServiceProvider();
        }

        protected int OnExecute(CommandLineApplication app)
        {
            return 1;
        }

        static async Task EnviaEmail(SendGridMessage sendGridMessage)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var configurationString = configurationBuilder.Build().GetValue<string>("SendGrid:apiKey");
            var client = new SendGridClient(configurationString);
            var response = await client.SendEmailAsync(sendGridMessage);
        }
    }
}
