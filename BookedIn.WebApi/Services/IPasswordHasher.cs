﻿namespace BookedIn.WebApi.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
    
    bool VerifyPassword(string hashedPassword, string password);
}

public class BCryptPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}