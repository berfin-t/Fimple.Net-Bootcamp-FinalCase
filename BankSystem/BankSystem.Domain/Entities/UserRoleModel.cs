using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Domain.Entities
{
    public class UserRoleModel
    {
        [Key]
        public string UserRoleId { get; set; }
        public string RoleName { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public UserModel Users { get; set; }
    }
}
