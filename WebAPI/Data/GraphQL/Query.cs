using WebAPI.Data.Repositories;
using WebAPI.Models;

namespace WebAPI.Data.GraphQL
{
    public class Query
    {
        public List<User> GetAllUsers([Service] UserRepository userRepository) 
        {
            return userRepository.getAllUsers();
        }

        public List<Result> GetResultsByUser([Service] ResultRepository resultRepository, int userId) 
        {
            return resultRepository.GetResultsByUser(userId);
        }

        public List<Result> GetAllResults([Service] ResultRepository resultRepository) 
        {
            return resultRepository.GetAllResults();
        }
    }
}
