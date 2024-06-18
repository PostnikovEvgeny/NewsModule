using NewsModule.Data;
using NewsModule.Models;
using NewsModule.Services.Jwt;

namespace NewsModule.Services
{
    public class RegisterService
    {
        JwtProvider jwtProvider = new JwtProvider();
        PasswordHasher hasher = new PasswordHasher();
        NewsModuleContext context;

        public RegisterService(NewsModuleContext context)
        {
            this.context = context;
        }
        public void Register(int id, string userName, string email, string password, enumRoles role)
        {
            User user=null;
            using (context)
            {
                if (email != null)
                {
                    user = context.Users.FirstOrDefault(p => p.Email == email);
                }
            }
            if (user == null)
            {
                string hashedPassword = hasher.Generate(password);

                User newUser = new User(id, userName, email, hashedPassword, role);
                using (context)
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

            }
        }
            
        
        /*public string Login(string email,string password)
        {
            User user = new User();
            using (context)
            {
                if (email != null)
                {
                    user = context.Users.FirstOrDefault(p => p.Email == email);
                }   
            }
            var result = hasher.Verify(password,user.PasswordHash);

            if (result == null)
            {
                throw new Exception("failed to login");
            }


            var token = jwtProvider.GenerateToken(user);

            return token;   

        }*/
    }
}
