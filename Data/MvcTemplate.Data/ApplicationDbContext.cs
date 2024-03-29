﻿namespace MvcTemplate.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using MvcTemplate.Data.Common.Models;
    using MvcTemplate.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<BookCategory> BooksCategories { get; set; }

        public IDbSet<BookAuthor> BookAuthors { get; set; }

        public IDbSet<BookAuthorBooks> BookAuthorBooks { get; set; }

        public IDbSet<BookContent> BookContents { get; set; }

        public IDbSet<HtmlPagingItem> HtmlPagingItems { get; set; }

        public IDbSet<NavigationItem> NavigationItems{ get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
