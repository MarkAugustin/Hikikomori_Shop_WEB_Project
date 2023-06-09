﻿namespace HikikomoriShop.Domain;

public class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string? RoleId { get; set; }
    public Role? Role { get; set; }
}