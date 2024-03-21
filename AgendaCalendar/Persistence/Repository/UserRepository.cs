using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using AgendaCalendar.Persistence.Data;
using System.Linq.Expressions;

namespace AgendaCalendar.Persistence.Repository
{
    public class UsersRepository : IRepository<User>
    {
        private readonly AppDbContext _dbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Add(user);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var userToDelete = _dbContext.Users.Find(x => x.Id == id);
            _dbContext.Users.Remove(userToDelete);
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<IReadOnlyList<User>> GetListAsync(CancellationToken cancellationToken = default)
        {
           // var query = _dbContext.Users.AsQueryable();
            return _dbContext.Users.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<User>> ListAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Users.AsQueryable();
            if (filter != null) query = query.Where(filter);

            return query.ToList();
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            var myUser = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
            myUser.Update(user);
            return myUser;
        }
    }
}