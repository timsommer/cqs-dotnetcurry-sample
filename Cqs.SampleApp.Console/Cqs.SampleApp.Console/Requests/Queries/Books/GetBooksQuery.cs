using System.Collections.Generic;
using Cqs.SampleApp.Core.Cqs.Data;
using Cqs.SampleApp.Core.Domain;

namespace Cqs.SampleApp.Console.Requests.Queries.Books
{
    public class GetBooksQuery : Query
    {
        public bool ShowOnlyInPossession { get; set; }
        
        //other filters here
    }

    public class GetBooksQueryResult : IResult
    {
        public IEnumerable<Book> Books { get; set; }
    }
}