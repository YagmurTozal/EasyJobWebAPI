
namespace EasyJobWebAPI.Model.AuthenticationAPIModel
{
    public class User
    {
        public string Email { get; set; } = string.Empty;

        public byte[]? UserHash { get; set; }

        public byte[]? UserSalt { get; set; }
    }
}