using WebAPI.Models;

namespace WebAPI.Data.Repositories
{
    public class ActivityRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public ActivityRepository(StoreDbContext dbContext)
        {
            _storeDbContext = dbContext;
        }

        public List<Activity> GetAllActivities()
        {
            return _storeDbContext.Activities.ToList();
        }
    }
}
