using ExampleTest2.Data;
using ExampleTest2.Models;

namespace ExampleTest2.Services;

public class DbService : IDbService
{
    private readonly ApplicationContext _context;
    
    public DbService(ApplicationContext context)
    {
        _context = context;
    }
}