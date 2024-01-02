using System.ComponentModel.DataAnnotations;

namespace BankSystem.Domain.Entities
{
    public class AccountModel
    {
        [Key]
        //public string AccountId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string UserId { get; set; }
        public decimal NewBalance { get; set; }
    }
}
