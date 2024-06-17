namespace NewsModule.Services
{
    public class PasswordHasher
    {
        //хэширование с помощью BCrypt
        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        
    }
}
