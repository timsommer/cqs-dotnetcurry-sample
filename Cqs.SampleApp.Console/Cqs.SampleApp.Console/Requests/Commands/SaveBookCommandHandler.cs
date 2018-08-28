using System.Data.Entity;
using Cqs.SampleApp.Core.Constants;
using Cqs.SampleApp.Core.Cqs;
using Cqs.SampleApp.Core.DataAccess;

namespace Cqs.SampleApp.Console.Requests.Commands
{
    public class SaveBookCommandHandler : CommandHandler<SaveBookCommand, SaveBookCommandResult>
    {
        public SaveBookCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        protected override SaveBookCommandResult DoHandle(SaveBookCommand request)
        {
            var _response = new SaveBookCommandResult();

            //get the book
            ApplicationDbContext.Books.Attach(request.Book);

            //add or update the book entity
            ApplicationDbContext.Entry(request.Book).State =
                request.Book.Id == Constants.NewId ? EntityState.Added : EntityState.Modified;
            
            //persist changes to the datastore
            ApplicationDbContext.SaveChanges();

            return _response;
        }
    }
}