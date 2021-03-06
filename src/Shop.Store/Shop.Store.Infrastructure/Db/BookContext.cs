﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Shared.Domain;
using Shop.Shared.Domain.Event;
using Shop.Shared.Model;
using Shop.Shared.SeedWork;
using Shop.Store.Core.Book;
using Shop.Store.Core.BookContent;
using Shop.Store.Core.Price;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Infrastructure.Db
{
    public class BookContext : DbContext
    {
        #region DbSets
        public DbSet<Books> Books { get; set; }
        public DbSet<Content> BookContents { get; set; }
        public DbSet<BookCosts> BookCosts { get; set; }
        public DbSet<Author> Authors { get; set; }
        #endregion
        private readonly DatabaseOption _databaseOption;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        public BookContext(DbContextOptions options, DatabaseOption databaseOption, ILoggerFactory loggerFactory, IDomainEventDispatcher domainEventDispatcher) :
            base(options)
        {
            _databaseOption = databaseOption;
            _loggerFactory = loggerFactory;
            _domainEventDispatcher = domainEventDispatcher;
        }
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
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Entity)
                .Select(x => (Entity)x.Entity).ToList();
            if (entities.Count > 0)
                entities.ForEach(async x => await _domainEventDispatcher.Dispatch(x.DomainEvents.ToArray()));
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}