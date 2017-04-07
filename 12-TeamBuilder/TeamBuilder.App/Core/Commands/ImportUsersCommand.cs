namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml.Linq;
    using Utilites;

    class ImportUsersCommand
    {
        // ImportUsers <filePathToXmlFile>
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(1, inputArgs);

            string filePath = inputArgs[0];

            // Validate file path
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format(Constants.ErrorMessages.FileNotFound, filePath));
            }

            // Read users from XML
            List<User> users;
            try
            {
                users = GetUsersFromXml(filePath);
            }
            catch (Exception)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidXmlFormat);
            }

            // Add users to database
            AddUsers(users);

            return $"You have successfully imported {users.Count} users!";
        }

        private void AddUsers(List<User> users)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private List<User> GetUsersFromXml(string filePath)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                XDocument xmlDoc = XDocument.Load(filePath);
                var usersData = xmlDoc.Root.Elements();

                List<User> users = new List<User>();
                foreach (XElement u in usersData)
                {
                    // Convert gender to title case
                    string genderTitleCase = CultureInfo.CurrentCulture.TextInfo
                                            .ToTitleCase(u.Element("gender").Value);
                    Gender gender = (Gender)Enum.Parse(typeof(Gender), genderTitleCase);

                    // User
                    User user = new User()
                    {
                        Username = u.Element("username").Value,
                        Password = u.Element("password").Value,
                        FirstName = u.Element("first-name").Value,
                        LastName = u.Element("last-name").Value,
                        Age = int.Parse(u.Element("age").Value),
                        Gender = gender
                    };
                    users.Add(user);
                }

                return users;
            }
        }
    }
}
