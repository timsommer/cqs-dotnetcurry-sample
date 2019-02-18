using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqs.SampleApp.Console.Infrastructure;
using Cqs.SampleApp.Console.Requests.Commands;
using Cqs.SampleApp.Console.Requests.Queries.Books;
using Cqs.SampleApp.Core.Cqs;
using Cqs.SampleApp.Core.DataAccess;
using Cqs.SampleApp.Core.Domain;
using Cqs.SampleApp.Core.IoC;
using log4net;

namespace Cqs.SampleApp.Console
{
    internal class Program
    {
        private static readonly ILog _Log = LogManager.GetLogger(typeof(Program).Name);
        
        private static void Main(string[] args)
        {

            _Log.Info("Bootsrapping application..");

            var _container = Bootstrapper.Bootstrap();

            // WithoutCqs(_container);

            // WithCqs(_container);

            WithCqsAsync(_container);

            System.Console.ReadLine();
        }

        private static void WithCqs(IAutofacContainer container)
        {
            var _commandDispatcher = container.Resolve<ICommandDispatcher>();
            var _queryDispatcher = container.Resolve<IQueryDispatcher>();

            var _response = _queryDispatcher.Dispatch<GetBooksQuery, GetBooksQueryResult>(new GetBooksQuery());

            _Log.Info("Retrieving all books the CQS Way..");

            foreach (var _book in _response.Books)
            {
                _Log.InfoFormat("Title: {0}, Authors: {1}, InMyPossession: {2}", _book.Title, _book.Authors, _book.InMyPossession);
            }
            
            //edit first book
            var _bookToEdit = _response.Books.First();
            _bookToEdit.InMyPossession = !_bookToEdit.InMyPossession;
            _commandDispatcher.Dispatch<SaveBookCommand, SaveBookCommandResult>(new SaveBookCommand()
            {
                Book = _bookToEdit
            });


            //add new book
            _commandDispatcher.Dispatch<SaveBookCommand, SaveBookCommandResult>(new SaveBookCommand()
            {
                Book = new Book()
                {
                    Title = "C# in Depth",
                    Authors = "Jon Skeet",
                    InMyPossession = false,
                    DatePublished = new DateTime(2013, 07, 01)
                }
            });


            _response = _queryDispatcher.Dispatch<GetBooksQuery, GetBooksQueryResult>(new GetBooksQuery());

            foreach (var _book in _response.Books)
            {
                _Log.InfoFormat("Title: {0}, Authors: {1}, InMyPossession: {2}", _book.Title, _book.Authors, _book.InMyPossession);
            }
        }

        private static async void WithCqsAsync(IAutofacContainer container)
        {
            var _commandDispatcher = container.Resolve<ICommandDispatcher>();
            var _queryDispatcher = container.Resolve<IQueryDispatcher>();

            var _response = await _queryDispatcher.DispatchAsync<GetBooksQuery, GetBooksQueryResult>(new GetBooksQuery());

            _Log.Info("Retrieving all books the CQS Way..");

            foreach (var _book in _response.Books)
            {
                _Log.InfoFormat("Title: {0}, Authors: {1}, InMyPossession: {2}", _book.Title, _book.Authors, _book.InMyPossession);
            }

            //edit first book
            var _bookToEdit = _response.Books.First();
            _bookToEdit.InMyPossession = !_bookToEdit.InMyPossession;
            await _commandDispatcher.DispatchAsync<SaveBookCommand, SaveBookCommandResult>(new SaveBookCommand()
            {
                Book = _bookToEdit
            });


            //add new book
            await _commandDispatcher.DispatchAsync<SaveBookCommand, SaveBookCommandResult>(new SaveBookCommand()
            {
                Book = new Book()
                {
                    Title = "C# in Depth",
                    Authors = "Jon Skeet",
                    InMyPossession = false,
                    DatePublished = new DateTime(2013, 07, 01)
                }
            });


            _response = await _queryDispatcher.DispatchAsync<GetBooksQuery, GetBooksQueryResult>(new GetBooksQuery());

            foreach (var _book in _response.Books)
            {
                _Log.InfoFormat("Title: {0}, Authors: {1}, InMyPossession: {2}", _book.Title, _book.Authors, _book.InMyPossession);
            }

        }

        private static void WithoutCqs(IAutofacContainer container)
        {

            //resolve context
            var _context = container.Resolve<ApplicationDbContext>();
            
            //save some books if there are none in the database
            if (!_context.Books.Any())
            {
                _context.Books.Add(new Book()
                {
                    Authors = "Andrew Hunt, David Thomas",
                    Title = "The Pragmatic Programmer",
                    InMyPossession = true,
                    DatePublished = new DateTime(1999, 10, 20),
                });

                _context.Books.Add(new Book()
                {
                    Authors = "Robert C. Martin",
                    Title = "The Clean Coder: A Code of Conduct for Professional Programmers",
                    InMyPossession = false,
                    DatePublished = new DateTime(2011, 05, 13),
                });

                _context.SaveChanges();

                _Log.Info("Books saved..");
            }

            _Log.Info("Retrieving all books the NON CQS Way..");

            foreach (var _book in _context.Books)
            {
                _Log.InfoFormat("Title: {0}, Authors: {1}, InMyPossession: {2}", _book.Title, _book.Authors, _book.InMyPossession);
            }
        }
    }
}
