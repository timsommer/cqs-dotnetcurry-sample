using System.Data.Entity;
using Cqs.SampleApp.Core.Constants;
using Cqs.SampleApp.Core.Cqs;
using Cqs.SampleApp.Core.DataAccess;

namespace Cqs.SampleApp.Console.Requests.Commands
{
    public class UpdateBookCommandHandler : CommandHandler<UpdateBookCommand, UpdateBookCommandResult>
    {
        public UpdateBookCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        protected override UpdateBookCommandResult DoHandle(UpdateBookCommand request)
        {
            var _response = CreateTypedResult();

            //get the book
            ApplicationDbContext.Books.Attach(request.Book);

            //add or update the book entity
            ApplicationDbContext.Entry(request.Book).State =
                request.Book.Id == Constants.NewId ? EntityState.Added : EntityState.Modified;
            
            //persist changes to the datastor
            ApplicationDbContext.SaveChanges();

            return _response;
        }
    }
}