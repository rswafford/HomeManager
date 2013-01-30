﻿namespace HomeManager.Domain.Services
{
    public interface ICryptoService
    {
        string GenerateSalt();
        string EncryptPassword(string password, string salt);
    }
}