using AgendaCalendar.Application.Abstractions;
using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;

namespace AgendaCalendar.Application.Services
{
    public class UserService : IService<User>
    {
        private IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork) 
        { 
            this.unitOfWork = unitOfWork;
        }
        public int Auth(string userName, string password)
        {
            try
            {
                if (userName == string.Empty || password == string.Empty) return -1;

                //generate JWT with ASP .NET CORE
                var userList = unitOfWork.UserRepository.ListAsync(x => x.UserName == userName).Result;
                if (!userList.Any())
                {
                    Console.WriteLine($"Cannot find user with such name : {userName}");
                    return -1;
                }
                var user = userList.First();
                if (user.Password == password)
                {
                    Console.WriteLine($"User {userName} succesfully authorised");
                    return user.Id;
                }
                else Console.WriteLine("Access denied");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception : {ex.Message}");
                return -1;
            }
            return -1;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await unitOfWork.UserRepository.GetListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) return;
            await unitOfWork.UserRepository.DeleteAsync(id);
        }

        public async Task AddAsync(User item)
        {
            try
            {
/*                var userWithSuchId = await unitOfWork.UserRepository.ListAsync(x => x.Id == item.Id);
                if (!userWithSuchId.Any())
                {
                    Console.WriteLine($"User with id {item.Id} already exists");
                    return;
                }*/
                var usersWithSuchName = await unitOfWork.UserRepository.ListAsync(x => x.UserName == item.UserName);
                if (usersWithSuchName.Any())
                {
                    Console.WriteLine($"Name {item.UserName} is not unique");
                    return;
                }
                if (item.Id < 0 || item.Password == string.Empty || item.UserName == string.Empty || item.Email == string.Empty)
                {
                    Console.WriteLine($"Empty strings");
                }

                await unitOfWork.UserRepository.AddAsync(item);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return;
            }
            await unitOfWork.SaveAllAsync();
        }

        public Task<User> UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }
    }
}