namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilites;

    class RegisterCommand
    {
        // RegisterUser <username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(7, inputArgs);

            string username = inputArgs[0];
            string password = inputArgs[1];
            string repeatPassword = inputArgs[2];
            string firstName = inputArgs[3];
            string lastName = inputArgs[4];
            int age;
            bool isNumberAge = int.TryParse(inputArgs[5], out age);
            Gender gender;
            bool isValidGender = Enum.TryParse(inputArgs[6], out gender);

            // Validate username
            if (username.Length < Constants.MinUsernameLength ||
                username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            // Validate password
            if (password.Length < Constants.MinPasswordLength ||
                password.Length > Constants.MaxFirstNameLength ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsUpper))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            // Validate passwords
            if (password != repeatPassword)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.PasswordDoesNotMatch));
            }

            // Validate first & last names
            if (firstName.Length > Constants.MaxFirstNameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.FirstNameNotValid, firstName));
            }
            if (lastName.Length > Constants.MaxLastNameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.LastNameNotValid, lastName));
            }

            // Validate age
            if (!isNumberAge || age <= 0)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.AgeNotValid));
            }

            // Validate gender
            if (!isValidGender)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.GenderNotValid));
            }

            // Check if username is not already taken 
            if (CommandHelper.IsUserExisting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            // Register user
            this.Register(username, password, firstName, lastName, age, gender);

            return $"User {username} was registered successfully!";
        }

        private void Register(string username, string password, string firstName, string lastName, int age, Gender gender)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = new User()
                {
                    Username = username,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Gender = gender
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
