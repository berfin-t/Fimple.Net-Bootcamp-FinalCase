using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Application.Dto
{
    public class UserDto
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Usersurname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public decimal AnnualIncome { get; set; }
        public decimal TotalAssets { get; set; }

        [JsonIgnore]
        public List<LoginDto> LoginDtos { get; set; }
        [JsonIgnore]
        public List<AccountDto> AccountDtos { get; set; }
    }
}
