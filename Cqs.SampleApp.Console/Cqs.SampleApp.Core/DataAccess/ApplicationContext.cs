using System.Data.Entity;
using Cqs.SampleApp.Core.Domain;

namespace Cqs.SampleApp.Core.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public virtual IDbSet<Book> Books => Set<Book>();
    }
}