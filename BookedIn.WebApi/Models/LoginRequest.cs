﻿namespace BookedIn.WebApi.Models;

public class LoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}