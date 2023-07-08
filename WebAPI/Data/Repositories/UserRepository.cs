using WebAPI.Models;

namespace WebAPI.Data.Repositories
{
    public class UserRepository
    {
        private readonly StoreDbContext _storeDbContext;
        
        public UserRepository(StoreDbContext dbContext)
        {
            _storeDbContext = dbContext;
        }

        public List<User> getAllUsers()
        {
            return _storeDbContext.Users.ToList();
        }
    }
}
