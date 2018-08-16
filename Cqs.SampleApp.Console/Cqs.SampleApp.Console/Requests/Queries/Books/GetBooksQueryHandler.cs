using System.Linq;
using Cqs.SampleApp.Core.Cqs;
using Cqs.SampleApp.Core.DataAccess;

namespace Cqs.SampleApp.Console.Requests.Queries.Books
{
    public class GetBooksQueryHandler : QueryHandler<GetBooksQuery, GetBooksQueryResult>
    {
        public GetBooksQueryHandler(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        protected override GetBooksQueryResult Handle(GetBooksQuery request)
        {
            var _result = CreateTypedResult();

            _result.Books = ApplicationDbContext.Books.ToList();

            return _result;
        }
    }
}