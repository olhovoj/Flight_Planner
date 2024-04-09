namespace FlightPlanner.Core.Models;

public class PageResult<T> where T : Entity
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
    public List<T>? Items { get; set; }
}