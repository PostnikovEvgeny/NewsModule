using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NewsModule.Services.Jwt
{
    public class JwtOptions
    {
        public  string issuer = "MyAuthServer"; // издатель токена
        public  string audience = "MyAuthClient"; // потребитель токена
        string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
