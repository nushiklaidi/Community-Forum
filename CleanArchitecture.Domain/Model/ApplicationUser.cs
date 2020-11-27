using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArchitecture.Domain.Model
{
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; }

        public string ProfileImageUrl { get; set; }

        public DateTime MemberSince { get; set; }

        public bool IsActive { get; set; }
    }
}
