using AutoMapper;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using BankSystem.WebApi.JwtTokenOperation;

namespace BankSystem.Business.Repositories
{
    public class LoginRepository
    {
        private readonly BankingDbContext _context;
        private readonly JwtToken _token;
        private readonly IMapper _mapper;

        public LoginRepository(IMapper mapper, BankingDbContext context, JwtToken token)
        {
            _context = context;
            _mapper = mapper;
            _token = token;
        }
        public void AddLogin(LoginModel model)
        {
            model.Password = _token.HashPassword(model.Password);
            _context.Login.Add(model);
            _context.SaveChanges();
        }
        public UserModel GetUserByUsernameAndPassword(string username, string password)
        {
            var hashedPassword = _token.HashPassword(password);
            return _context.User.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
        }
    }
}
