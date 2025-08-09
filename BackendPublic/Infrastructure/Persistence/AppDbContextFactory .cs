using System;
using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Configura aquí tu cadena de conexión directamente para las migraciones
            optionsBuilder.UseSqlServer("Server=localhost;Database=HotelDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");


            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
