using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NomadDashboardsAPI.Models
{
    public class User : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Website { get; set; }


        [Column(TypeName = "nvarchar(150)")]
        public string ComponyName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Country { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string ZipCode { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Province { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LastLoginIp { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string CreatedAt { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ProfilePic { get; set; }
    }
}