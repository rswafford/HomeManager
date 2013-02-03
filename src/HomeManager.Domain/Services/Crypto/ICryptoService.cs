namespace HomeManager.Domain.Services.Crypto
{
    public interface ICryptoService
    {
        string GenerateSalt();
        string EncryptPassword(string password, string salt);
    }
}