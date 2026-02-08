using Microsoft.EntityFrameworkCore;
using TriporaProject.Models;


namespace TriporaProject.Services
{
    public class AuthService
    {
        private readonly TriporaDbContext _db;

        public User? CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;

        public AuthService(TriporaDbContext db)
        {
            _db = db;
        }

        // ===== REGISTER =====
        public async Task<(bool ok, string message)> RegisterAsync(
            string name,
            string email,
            string password,
            string? phone = null)
        {
            name = (name ?? "").Trim();
            email = (email ?? "").Trim();
            password = password ?? "";

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Please enter your name.");

            if (string.IsNullOrWhiteSpace(email))
                return (false, "Please enter your email.");

            if (string.IsNullOrWhiteSpace(password))
                return (false, "Please enter your password.");

            var exists = await _db.Users.AnyAsync(u => u.Email == email);
            if (exists)
                return (false, "Email already registered.");

            var user = new User
            {
                Name = name,
                Email = email,
                Password = password,   // LECTURE STYLE (plain text)
                Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim(),
                Role = "User"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            CurrentUser = user;
            return (true, "Account created and logged in.");
        }

        // ===== LOGIN =====
        public async Task<(bool ok, string message)> LoginAsync(string email, string password)
        {
            email = (email ?? "").Trim();
            password = password ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return (false, "Please enter email and password.");

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
                return (false, "Invalid email or password.");

            CurrentUser = user;
            return (true, "Login successful.");
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
