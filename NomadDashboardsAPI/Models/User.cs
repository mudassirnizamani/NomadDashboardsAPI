﻿using Microsoft.AspNetCore.Identity;
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
        public string Position { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string ComponyName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Country { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string ComponyAddress { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public int ZipCode { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string State { get; set; }
    }
}