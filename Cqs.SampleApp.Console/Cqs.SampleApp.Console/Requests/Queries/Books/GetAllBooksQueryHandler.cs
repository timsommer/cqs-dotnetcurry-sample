using System.Linq;
using Cqs.SampleApp.Core.Cqs;
using Cqs.SampleApp.Core.DataAccess;

namespace Cqs.SampleApp.Console.Requests.Queries.Books
{
    public class GetAllBooksQueryHandler : QueryHandler<GetAllBooksQuery, GetAllBooksQueryResult>
    {
        public GetAllBooksQueryHandler(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        protected override GetAllBooksQueryResult Handle(GetAllBooksQuery request)
        {
            var _result = CreateTypedResult();

            _result.Books = ApplicationDbContext.Books.ToList();

            return _result;
        }
    }
}