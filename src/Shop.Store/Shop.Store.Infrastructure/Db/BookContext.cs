using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Shared.Model;
using Shop.Shared.SeedWork;
using Shop.Store.Core.Book;

namespace Shop.Store.Infrastructure.Db
{
    public class BookContext : DbContext
    {
        private readonly DatabaseOption _databaseOption;
        private readonly ILoggerFactory _loggerFactory;

        public BookContext(DbContextOptions options, DatabaseOption databaseOption, ILoggerFactory loggerFactory) :
            base(options)
        {
            _databaseOption = databaseOption;
            _loggerFactory = loggerFactory;
        }

        public DbSet<BookInfo> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_databaseOption.InMemmory)
            {
                optionsBuilder.UseInMemoryDatabase("BookDb");
                return;
            }

            optionsBuilder.UseSqlServer(_databaseOption.ConnectionString);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookContext).Assembly);
            modelBuilder.AddStronglyTypedIdConversions();
        }
    }
}