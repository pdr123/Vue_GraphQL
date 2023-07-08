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
            return _storeDbContext.Result
                .Include(a => a.Status)
                .Include(a => a.Activity).ThenInclude(u => u.User)
                .Include(u => u.User)
                .ToList();
        }

        public List<Result> GetResultsByUser(int userId)
        {
            return _storeDbContext.Result
                .Include(a => a.Activity).ThenInclude(u => u.User)
                .Include(s => s.Status)
                .Include(u => u.User)
                .Where(result => result.UserId == userId).ToList();
        }
    }
}
