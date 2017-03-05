using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _08_Create_User.Models
{
    public class User
    {
        private string username;
        private string password;
        private string email;
        private int? fileSize;
        private int? age;

        public User()
        {
        }
        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4), StringLength(30)]
        public string Username
        {
            get { return this.username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username is required");
                }
                value = value.Trim();

                if (value.Length < 4 || value.Length > 30)
                {
                    throw new ArgumentException("Username min legnth = 4, max length = 30");
                }
                this.username = value;
            }
        }

        [Required]
        [MinLength(6), MaxLength(50)]
        public string Password
        {
            get { return this.password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password is required");
                }
                value = value.Trim();

                if (value.Length < 6 || value.Length > 50)
                {
                    throw new ArgumentException("Password min legnth = 6, max length = 50");
                }

                // Regex validation
                if (!new Regex(@"[a-z]+").IsMatch(value))
                {
                    throw new ArgumentException("Password must contain at least 1 lowercase letter");
                }

                if (Regex.Matches(value, @"[A-Z]+").Count == 0)
                {
                    throw new ArgumentException("Password must contain at least 1 uppercase letter");
                }

                if (!Regex.IsMatch(value, @"\d+"))
                {
                    throw new ArgumentException("Password must contain at least 1 digit");
                }

                if (!Regex.IsMatch(value, @"[!@#$%^&*()_+<>?]+"))
                {
                    throw new ArgumentException("Password must contain at lease 1 special symbol");
                }

                //if (!new Regex(@"[!@#$%^&*()_+<>?]+").IsMatch(value))
                //{
                //    throw new ArgumentException("Password must contain at lease 1 special symbol");
                //}
                this.password = value;
            }
        }

        [Required]
        public string Email
        {
            get { return this.email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email is required");
                }
                value = value.Trim();

                // regex vallidation
                if (!new Regex(@"^[a-zA-Z\d]+[a-zA-Z\d._-]*[a-zA-Z\d]+@[a-zA-Z]+\.[a-zA-Z]+$").IsMatch(value))
                {
                    throw new ArgumentException("Email does not match email pattern");
                }
                this.email = value;
            }
        }

        public int? ProfilePicture // or varbinary ?
        {
            get { return this.fileSize; }
            set
            {
                if (value < 0 || value > 1000000)
                {
                    throw new ArgumentException("Image max file size = 1MB (1,000,000 b)");
                }
                this.fileSize = value;
            }
        }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        public int? Age
        {
            get { return this.age; }
            set
            {
                if (value < 1 || value > 120)
                {
                    throw new ArgumentException("Age min value = 1, max value = 120");
                }
                this.age = value;
            }
        }

        public bool? IsDeleted { get; set; }
    }
}
