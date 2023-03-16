using app1.DTO;
using app1.Model;

namespace app1.Repository.RepositoryInterface
{
    public interface ILocalUsers
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseLocalUsersDTO> Login(LoginRequestLocalUsersDTO loginRequestLocalUsersDTO);
        Task<LocalUserModel> Register(RegistrerationRequestLocalUsersDTO registrerationRequestLocalUsersDTO);
    }
}
