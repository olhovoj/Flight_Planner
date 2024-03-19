using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Data;

public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
{
    public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : 
        base(options)
    {
        
    }
    
    public DbSet<Flight> Flights { get; set; }
    public void ExecuteSqlRaw(string sql, params object[] parameters)
    {
        Database.ExecuteSqlRaw(sql, parameters);
    }

    public DbSet<Airport> Airports { get; set; }
}