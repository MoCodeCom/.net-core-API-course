using app1.DTO;
using app1.Model;
using app1.Repository.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace app1.Repository
{
    public class LocalUsers:ILocalUsers
    {
        private readonly ApplicationDbContext _context;
        private string _secretKey;
        public LocalUsers(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _secretKey = config.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var User = _context.localUser.FirstOrDefault(u => u.Username == username);
            return User == null ? true : false;
        }

        public async Task<LoginResponseLocalUsersDTO> Login(LoginRequestLocalUsersDTO loginRequestLocalUsersDTO)
        {
            // LoginResponseLocalUsersDTO = User<localusermodel>, token<string>
            var user = _context.localUser.FirstOrDefault(u =>
                u.Username == loginRequestLocalUsersDTO.userName.ToLower() &&
                u.Passwrod == loginRequestLocalUsersDTO.password
                ) ;

            if (user == null)
            {
                //return null;
                return new LoginResponseLocalUsersDTO()
                {
                    Tocken = "",
                    User = null
                };
            }


            //if user found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler(); // it is security token
            var key = Encoding.ASCII.GetBytes(_secretKey); // string

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
             {
                 //claim build on the data form request parameter.
                 new Claim(ClaimTypes.Name, user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role)
             }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); // this token without serialized 

            LoginResponseLocalUsersDTO userResponseDto = new LoginResponseLocalUsersDTO()
            {
                Tocken = tokenHandler.WriteToken(token), // serialized token to string
                User = user
            };
            

            return userResponseDto;
        }

        public async Task<LocalUserModel> Register(RegistrerationRequestLocalUsersDTO registrerationRequestLocalUsersDTO)
        {
            LocalUserModel user = new()
            {
                Username = registrerationRequestLocalUsersDTO.UserName,
                Passwrod = registrerationRequestLocalUsersDTO.Password,
                Name = registrerationRequestLocalUsersDTO.Name,
                Role = registrerationRequestLocalUsersDTO.Role
            };

            _context.localUser.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
