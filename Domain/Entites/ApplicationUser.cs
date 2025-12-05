using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class ApplicationUser : IdentityUser, IAuditable
    {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LastActiveTenantSubDomain { get; set; } = string.Empty;
    public bool HasOnboarded { get; set; }
    public bool IsSubscribed { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    }
}
