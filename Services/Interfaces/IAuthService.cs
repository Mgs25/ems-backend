using ems_backend.Entities;

namespace ems_backend.Services
{
    public interface IAuthService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateRandomToken();
        string CreateAccessToken(User user);
        public string GenerateOtp();
    }
}