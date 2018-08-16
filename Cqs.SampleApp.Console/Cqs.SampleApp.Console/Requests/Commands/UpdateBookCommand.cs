using System.Linq;
using Cqs.SampleApp.Core.Cqs.Data;
using Cqs.SampleApp.Core.Domain;

namespace Cqs.SampleApp.Console.Requests.Commands
{
    public class UpdateBookCommand : Command
    {
        public Book Book { get; set; }
    }

    public class UpdateBookCommandResult : IResult
    {
    }
}