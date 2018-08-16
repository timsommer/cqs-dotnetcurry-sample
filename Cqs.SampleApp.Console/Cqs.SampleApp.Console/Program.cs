using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqs.SampleApp.Console.Infrastructure;
using Cqs.SampleApp.Console.IoC;
using Cqs.SampleApp.Core.DataAccess;
using Cqs.SampleApp.Core.Domain;
using log4net;

namespace Cqs.SampleApp.Console
{
    internal class Program
    {
        private static readonly ILog _Log = LogManager.GetLogger(typeof(Program).Name);
        
        private static void Main(string[] args)
        {

            _Log.Info("Bootsrapping..");

            var _container = Bootstrapper.Bootstrap();

            WithoutCqs(_container);

            System.Console.ReadLine();
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
                    Author = "Andrew Hunt, David Thomas",
                    Title = "The Pragmatic Programmer",
                    Bought = true,
                    DatePublished = new DateTime(1999, 10, 20),
                });

                _context.Books.Add(new Book()
                {
                    Author = "Robert C. Martin",
                    Title = "The Clean Coder: A Code of Conduct for Professional Programmers",
                    Bought = false,
                    DatePublished = new DateTime(2011, 05, 13),
                });

                _context.SaveChanges();

                _Log.Info("Books saved..");
            }

            _Log.Info("Retrieving all books..");

            foreach (var _book in _context.Books)
            {
                _Log.InfoFormat("Title: {0}, Authors: {1}, Bought: {2}", _book.Title, _book.Author, _book.Bought);
            }
        }
    }
}
