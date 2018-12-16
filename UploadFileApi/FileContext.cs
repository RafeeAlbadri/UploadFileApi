using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFileApi.Model;

namespace UploadFileApi
{
    public class FileContext : DbContext
    {
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<User> Users { get; set; }



        public FileContext(DbContextOptions<FileContext> FileContextOptions)
           : base(FileContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}
