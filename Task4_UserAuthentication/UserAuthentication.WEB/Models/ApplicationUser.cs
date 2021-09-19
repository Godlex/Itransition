namespace UserAuthentication.WEB.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public DateTime? FirstLogin { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}