using Microsoft.EntityFrameworkCore;
using ToDoodle.Data;
using ToDoodle.Data.Model;
using ToDoodleBlazor.DTOs;
using ToDoodleBlazor.Responses;

namespace ToDoodleBlazor.Repos
{
    public class Account(ApplicationDbContext dbContext, IConfiguration config) : IAccount
    {
        public async Task<CustomResponses.LoginResponse> LoginAsync(LoginDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponses.RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var user = await GetUser(model.Email);
            if (user is not null)
                return new CustomResponses.RegistrationResponse(false, "User already exists.");

            dbContext.Users.Add(new()
            {
                Name = model.Name,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            });

            await dbContext.SaveChangesAsync();
            return new CustomResponses.RegistrationResponse(true, "Success");
        }

        private async Task<ApplicationUser?> GetUser(string email) =>
            await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}
