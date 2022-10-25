using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMusicStores.Models;

namespace MvcMusicStores.Data
{
    public class MvcMusicStoresContext : DbContext
    {
        public MvcMusicStoresContext (DbContextOptions<MvcMusicStoresContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMusicStores.Models.Album> Album { get; set; } 
        public DbSet<MvcMusicStores.Models.Genre> Genre { get; set; }
        public DbSet<MvcMusicStores.Models.Artist> Artist { get; set; }
    }
}
