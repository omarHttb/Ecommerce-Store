
using System;
using Core.entites;
using Infastructure.config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {




        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

        
    }

}



