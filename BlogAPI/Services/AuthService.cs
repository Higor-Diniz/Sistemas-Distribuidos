using BlogAPI.Data;
using BlogAPI.DTOs.Auth;
using BlogAPI.Entities;
using BlogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ServiceResponse<string>> RegisterAsync(UserRegistrationDto registrationDto)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == registrationDto.Email))
                {
                    return ServiceResponse<string>.FailureResponse("O E-mail já existe!");
                }

                var user = new User
                {
                    Username = registrationDto.Username,
                    Email = registrationDto.Email,
                    PasswordHash = HashPassword(registrationDto.Password),
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var token = GenerateJwtToken(user);
                return ServiceResponse<string>.SuccessResponse(token, "Usuário registrado com Sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o registro do usuário");
                return ServiceResponse<string>.FailureResponse($"O Registro falhou: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<string>> LoginAsync(UserLoginDto loginDto)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
                {
                    return ServiceResponse<string>.FailureResponse("E-mail ou Senha inválidos!");
                }

                var token = GenerateJwtToken(user);
                return ServiceResponse<string>.SuccessResponse(token, "Login realizado!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o login do usuário!");
                return ServiceResponse<string>.FailureResponse($"O Login falhou: {ex.Message}");
            }
        }

        private string HashPassword(string password)
        {
            return global::BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return global::BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Chave JWT não encontrada na configuração!")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
} 