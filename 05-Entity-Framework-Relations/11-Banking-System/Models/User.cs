using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _11_Banking_System.Models
{
    public class User // Problem 12
    {
        private string username;
        private string password;
        private string email;

        public User()
        {
            this.CheckingAccounts = new HashSet<CheckingAccount>();
            this.SavingAccounts = new HashSet<SavingAccount>();
        }
        public int Id { get; set; }

        [Required]
        public string Username
        {
            get { return this.username; }
            set
            {
                // Validation method used

                //if (string.IsNullOrWhiteSpace(value))
                //{
                //    throw new ArgumentException("Required Username");
                //}
                //if (!Regex.IsMatch(value, @"^[a-zA-Z][a-zA-Z\d]{2,}$"))
                //{
                //    throw new ArgumentException("Invalid Username");
                //}

                this.username = value;
            }
        }

        [Required]
        public string Password
        {
            get { return this.password; }
            set
            {
                // Validation method used

                //if (string.IsNullOrWhiteSpace(value))
                //{
                //    throw new ArgumentException("Required Password");
                //}
                //value = value.Trim();

                //if (!Regex.IsMatch(value, @"[a-z]") || 
                //    !Regex.IsMatch(value, @"[A-Z]") || 
                //    !Regex.IsMatch(value, @"\d") ||
                //    value.Length < 6)
                //{
                //    throw new ArgumentException("Invalid Password");
                //}

                this.password = value;
            }
        }

        [Required]
        public string Email {
            get { return this.email; }
            set
            {
                // Validation method used

                //if (string.IsNullOrWhiteSpace(value))
                //{
                //    throw new ArgumentException("Required Email");
                //}
                //if (!Regex.IsMatch(value, @"^([a-zA-Z0-9]+[-|_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[-]?)*[a-zA-Z0-9]+\.([a-zA-Z0-9]+[-]?)*[a-zA-Z0-9]+$"))
                //{
                //    throw new ArgumentException("Invalid Email");
                //}

                this.email = value;
            }
        }

        public virtual ICollection<CheckingAccount> CheckingAccounts { get; set; }

        public virtual ICollection<SavingAccount> SavingAccounts { get; set; }
    }
}
