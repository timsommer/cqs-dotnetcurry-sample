using System.Linq;
using Cqs.SampleApp.Core.Cqs.Data;
using Cqs.SampleApp.Core.Domain;

namespace Cqs.SampleApp.Console.Requests.Commands
{
    public class SaveBookCommand : Command
    {
        public Book Book { get; set; }
    }

    public class SaveBookCommandResult : IResult
    {
    }
}