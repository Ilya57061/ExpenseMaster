
namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IHashService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, out byte[] passwordSalt, byte[] passwordHash);
    }
}
