﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashControl.Models
{
    public partial class Users
    {
        public Users()
        {
            Transactions = new HashSet<Transactions>();
        }

        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
