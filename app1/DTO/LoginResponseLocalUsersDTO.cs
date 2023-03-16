using app1.Model;

namespace app1.DTO
{
    public class LoginResponseLocalUsersDTO
    {
        public LocalUserModel User { get; set; }
        public string Tocken { get; set; }
    }
}
