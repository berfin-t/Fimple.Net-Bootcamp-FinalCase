using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using BankSystem.WebApi.JwtTokenOperation;

namespace BankSystem.Application.Repositories
{
    public class UserRepository
    {
        private readonly BankingDbContext _context;
        private readonly JwtToken _token;
        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper,BankingDbContext context, JwtToken token)
        {
            _context = context;
            _mapper = mapper;
            _token = token;
        }

        public void AddUser(UserDto userDto)
        {
            var userModel = _mapper.Map<UserModel>(userDto);
            userModel.Password = _token.HashPassword(userModel.Password);
            _context.User.Add(userModel);
            _context.SaveChanges();
        }

        public UserModel GetUserByUsernameAndPassword(string username, string password)
        {
            var hashedPassword = _token.HashPassword(password);
            return _context.User.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
        }

        public void AddLogin(LoginModel model)
        {
            model.Password = _token.HashPassword(model.Password);
            _context.Login.Add(model);
            _context.SaveChanges();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {

            return _context.User.ToList();
        }

        public UserModel GetUserById(int userId)
        {
            return _context.User.Find(userId);
        }

        public void AssignUserRole(int userId, string role)
        {
            var user = _context.User.Find(userId);

            if (user != null)
            {
                user.Role = role;
                _context.SaveChanges();
            }
        }
    }
}
