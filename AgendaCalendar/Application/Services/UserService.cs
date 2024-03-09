using AgendaCalendar.Application.Abstractions;
using AgendaCalendar.Domain.Entities;
using AgendaCalendar.Persistence.Repository;

namespace AgendaCalendar.Application.Services
{
    public class UserService : IService<User>
    {
        private UsersRepository usersRepository;

        public UserService(UsersRepository repository) 
        { 
            usersRepository = repository;
        }
        public void Auth(string userName, string password)
        {
            if (userName == string.Empty || password == string.Empty) return;


            //generate JWT with ASP .NET CORE
            var user = usersRepository.ListAsync(x => x.UserName == userName).Result.First();
            if (user.Password == password)
            {
                Console.WriteLine($"User {userName} succesfully authorised");
            }
            else Console.WriteLine("Access denied");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await usersRepository.GetListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await usersRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await usersRepository.DeleteAsync(id);
        }

        public async Task AddAsync(User item)
        {
            await usersRepository.AddAsync(item);
        }

        public Task<User> UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }
    }
}