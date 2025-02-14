﻿namespace BookedIn.WebApi.Domain;

public record User
{
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required string Nickname { get; init; }
    public required DateTime DateOfBirth { get; init; }
    public required string PasswordHash { get; set; }
}