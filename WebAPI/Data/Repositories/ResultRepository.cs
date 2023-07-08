using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data.Repositories
{
    public class ResultRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public ResultRepository(StoreDbContext dbContext)
        {
            _storeDbContext = dbContext;
        }

        public List<Result> GetAllResults() 
        {
            return _storeDbContext.Results
                .Include(a => a.Activity)
                .Include(u => u.User)
                .Include(s => s.Status)
                .ToList();
        }

        public List<Result> GetResultsByUser(int userId)
        {
            return _storeDbContext.Results
                .Include(a => a.Activity)
                .Include(s => s.Status)
                .Where(result => result.UserId == userId).ToList();
        }
    }
}
