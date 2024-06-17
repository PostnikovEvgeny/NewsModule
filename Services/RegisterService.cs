using NewsModule.Data;
using NewsModule.Models;
using NewsModule.Services.Jwt;

namespace NewsModule.Services
{
    public class RegisterService
    {
        JwtProvider jwtProvider = new JwtProvider();
        PasswordHasher hasher = new PasswordHasher();

        public void Register(int id, string userName, string email, string password, string role)
        {
            User user=null;
            using (NewsModuleContext db = new NewsModuleContext())
            {
                if (email != null)
                {
                    user = db.Users.FirstOrDefault(p => p.Email == email);
                }
            }
            if (user == null)
            {
                string hashedPassword = hasher.Generate(password);

                User newUser = new User(id, userName, email, hashedPassword, role);
                using (NewsModuleContext db = new NewsModuleContext())
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }

            }
        }
            
        
        public string Login(string email,string password)
        {
            User user = new User();
            using (NewsModuleContext db = new NewsModuleContext())
            {
                if (email != null)
                {
                    user = db.Users.FirstOrDefault(p => p.Email == email);
                }   
            }
            var result = hasher.Verify(password,user.PasswordHash);

            if (result == null)
            {
                throw new Exception("failed to login");
            }


            var token = jwtProvider.GenerateToken(user);

            return token;   

        }
    }
}
