using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShopManagementBO
{
    public partial class User
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Name is required")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Name must be at least 6 characters long")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [StringLength(80, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        [Phone(ErrorMessage = "Invalid phone format.")]
        [RegularExpression(@"^((\+84)|0)(3[2-9]|5[2689]|7[06789]|8[1-9]|9[0-9])(\d{7})$", ErrorMessage = "Invalid phone format.")]
        public string? Phone { get; set; }
        public bool Enabled { get; set; }
        public int RoleId { get; set; }

        public virtual Role? Role { get; set; } = null!;
    }
}
