using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class EntityService<T> : DbService, IEntityService<T> where T: Entity
{
    protected static readonly object _lock = new object();
    
    public EntityService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public IEnumerable<T> GetAll()
    {
        return GetAll<T>();
    }

    public T? GetById(int id)
    {
        return GetById<T>(id);
    }

    public void Create(T entity)
    {
        lock (_lock)
        {
            Create<T>(entity);
        }
    }

    public void Delete(T entity)
    {
        Delete<T>(entity);
    }

    public void Update(T entity)
    {
        Update<T>(entity);
    }
}