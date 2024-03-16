using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Domain.Entities
{
    public class UserModel
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
        public ICollection<LoanModel> Loans { get; set; }

        [JsonIgnore]
        public  List<LoginModel> LoginModels { get; set; }
        [JsonIgnore]
        public List<AccountModel> Accounts { get; set; }
        [JsonIgnore]
        public List<LoanApplicationModel> LoanApplications { get; set; }
 
    }
}
