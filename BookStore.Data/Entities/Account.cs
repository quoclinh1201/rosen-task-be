using System;
using System.Collections.Generic;

namespace BookStore.Data.Entities
{
    public partial class Account
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? GoogleAccount { get; set; }
        public string Role { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public bool IsActive { get; set; }

        public virtual User? User { get; set; }
    }
}
