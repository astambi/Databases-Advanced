namespace _11_Banking_System.Core
{
    using Models;
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Utils;

    public class CommandProcessor
    {
        public string Execute(string input)
        {
            string[] inputArgs = Regex.Split(input, @"\s+");
            string command = input.Length != 0 ? inputArgs[0].ToLower() : string.Empty;
            inputArgs = inputArgs.Skip(1).ToArray();

            string output;
            switch (command)
            {
                case "register":        output = this.RegisterUser(inputArgs); break;
                case "login":           output = this.LoginUser(inputArgs); break;
                case "logout":          output = this.Logout(); break;
                case "exit":            output = this.Exit(); break;
                case "add":             output = this.AddAccount(inputArgs); break;
                case "deposit":         output = this.DepositMoney(inputArgs); break;
                case "withdraw":        output = this.WithdrawMoney(inputArgs); break;
                case "listaccounts":    output = this.ListAccounts(); break;
                case "deductfee":       output = this.DeductFee(inputArgs); break;
                case "addinterest":     output = this.AddInterest(inputArgs); break;
                default: throw new ArgumentException($"Command \"{command}\" not supported!");
            }

            return output;
        }

        private string RegisterUser(string[] input)
        {
            if (input.Length != 3)
            {
                throw new ArgumentException("Invalid input");
            }

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("Current user should log out before another user could log in");
            }

            // Register <username> <password> <email>
            string username = input[0];
            string password = input[1];
            string email = input[2];

            User user = new User()
            {
                Username = username,
                Password = password,
                Email = email
            };

            this.ValidateUser(user);

            using (BankingSystemContext context = new BankingSystemContext())
            {
                if (context.Users.Any(u => u.Username == username))
                {
                    throw new ArgumentException("Username already taken");
                }

                if (context.Users.Any(u => u.Email == email))
                {
                    throw new ArgumentException("Email already taken");
                }

                context.Users.Add(user);
                context.SaveChanges();
            }

            return $"User {username} registered successfully";
        }

        private string LoginUser(string[] input)
        {
            if (input.Length != 2)
            {
                throw new ArgumentException("Invalid input");
            }

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("Current user should logout first");
            }

            // Login <username> <password> 
            string username = input[0];
            string password = input[1];

            using (BankingSystemContext context = new BankingSystemContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password");
                }

                AuthenticationManager.Login(user);
            }

            return $"User {username} logged in successfully";
        }

        private string Logout()
        {
            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("User should log in first");
            }

            User user = AuthenticationManager.GetCurrentUser();
            AuthenticationManager.Logout();

            return $"User {user.Username} successfully logged out";
        }

        private string AddAccount(string[] input)
        {
            if (input.Length != 3)
            {
                throw new ArgumentException("Invalid input");
            }

            // Add SavingAccount <initial balance> <interest rate>
            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("User should log in first");
            }

            string accountType = input[0];

            string accountNumber = AccountGenerator.GenerateAccountNumber();
            decimal balance = decimal.Parse(input[1]);
            decimal rateOrFee = decimal.Parse(input[2]);

            if (accountType.Equals("CheckingAccount", StringComparison.OrdinalIgnoreCase))
            {
                CheckingAccount checkingAccount = new CheckingAccount()
                {
                    AccountNumber = accountNumber,
                    Balance = balance,
                    Fee = rateOrFee
                };

                this.ValidateCheckingAccount(checkingAccount);

                using (BankingSystemContext context = new BankingSystemContext())
                {
                    User user = AuthenticationManager.GetCurrentUser();
                    context.Users.Attach(user);
                    checkingAccount.User = user;

                    context.CheckingAccounts.Add(checkingAccount);
                    context.SaveChanges();
                }
            }
            else if (accountType.Equals("SavingAccount", StringComparison.OrdinalIgnoreCase))
            {
                SavingAccount savingAccount = new SavingAccount()
                {
                    AccountNumber = accountNumber,
                    Balance = balance,
                    InterestRate = rateOrFee
                };

                this.ValidateSavingAccount(savingAccount);

                using (BankingSystemContext context = new BankingSystemContext())
                {
                    User user = AuthenticationManager.GetCurrentUser();
                    context.Users.Attach(user);
                    savingAccount.User = user;

                    context.SavingAccounts.Add(savingAccount);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new ArgumentException($"Invalid account type {accountType}");
            }

            return $"Account number {accountNumber} successfully added";
        }

        private string DepositMoney(string[] input)
        {
            if (input.Length != 2)
            {
                throw new ArgumentException("Invalid input");
            }

            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("User should log in first");
            }

            // Deposit <Account number> <money> 
            string accountNumber = input[0];
            decimal amount = decimal.Parse(input[1]);
            if (amount <= 0)
            {
                throw new ArgumentException("Amount should be positive");
            }

            decimal currentBalance;

            using (BankingSystemContext context = new BankingSystemContext())
            {
                User user = context.Users.Attach(AuthenticationManager.GetCurrentUser());
                SavingAccount savingAccount = user.SavingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                CheckingAccount checkingAccount = user.CheckingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (savingAccount == null && checkingAccount == null)
                {
                    throw new ArgumentException($"Account {accountNumber} does not exist");
                }

                if (savingAccount != null)
                {
                    savingAccount.DepositMoney(amount);
                    context.SaveChanges();
                    currentBalance = savingAccount.Balance;
                }
                else
                {
                    checkingAccount.DepositMoney(amount);
                    context.SaveChanges();
                    currentBalance = checkingAccount.Balance;
                }
            }

            return $"Account {accountNumber} current balance = {currentBalance:f2}";
        }

        private string WithdrawMoney(string[] input)
        {
            if (input.Length != 2)
            {
                throw new ArgumentException("Invlaid input");
            }

            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("User should log in first");
            }

            // withdraw <Account number> <money> 
            string accountNumber = input[0];
            decimal amount = decimal.Parse(input[1]);
            if (amount <= 0)
            {
                throw new ArgumentException("Amount should be positive");
            }

            decimal currentBalance;

            using (BankingSystemContext context = new BankingSystemContext())
            {
                User user = context.Users.Attach(AuthenticationManager.GetCurrentUser());
                SavingAccount savingAccount = user.SavingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                CheckingAccount checkingAccount = user.CheckingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (savingAccount == null && checkingAccount == null)
                {
                    throw new ArgumentException($"Account {accountNumber} does not exist");
                }

                if (savingAccount != null)
                {
                    savingAccount.WithdrawMoney(amount);
                    context.SaveChanges();
                    currentBalance = savingAccount.Balance;
                }
                else
                {
                    checkingAccount.WithdrawMoney(amount);
                    context.SaveChanges();
                    currentBalance = checkingAccount.Balance;
                }
            }

            return $"Account {accountNumber} current balance = {currentBalance:f2}";
        }

        private string ListAccounts()
        {
            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("User should log in first");
            }

            StringBuilder builder = new StringBuilder();
            using (BankingSystemContext context = new BankingSystemContext())
            {
                User user = context.Users.Attach(AuthenticationManager.GetCurrentUser());

                builder.AppendLine("Saving Accounts:");
                foreach (SavingAccount userSavingAccount in user.SavingAccounts)
                {
                    builder.AppendLine($"--{userSavingAccount.AccountNumber} {userSavingAccount.Balance:f2}");
                }

                builder.AppendLine("Checking Accounts:");
                foreach (CheckingAccount checkingAccount in user.CheckingAccounts)
                {
                    builder.AppendLine($"--{checkingAccount.AccountNumber} {checkingAccount.Balance:f2}");
                }
            }

            return builder.ToString().Trim();
        }

        private string DeductFee(string[] input)
        {
            if (input.Length != 1)
            {
                throw new ArgumentException("Invalid input");
            }

            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("User should log in first");
            }

            // DeductFee <Account number> 
            string accountNumber = input[0];

            decimal currentBalance;

            using (BankingSystemContext context = new BankingSystemContext())
            {
                User user = context.Users.Attach(AuthenticationManager.GetCurrentUser());
                CheckingAccount checkingAccount = user.CheckingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (checkingAccount == null)
                {
                    throw new ArgumentException($"Account {accountNumber} does not exist");
                }
                
                checkingAccount.DeductFee();
                currentBalance = checkingAccount.Balance;
                context.SaveChanges();
            }

            return $"Fee deducted from account {accountNumber}, current balance = {currentBalance:f2}";
        }

        private string AddInterest(string[] input)
        {
            if (input.Length != 1)
            {
                throw new ArgumentException("Invalid input");
            }

            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("User should log in first");
            }

            // AddInterest <Account number> 
            string accountNumber = input[0];

            decimal currentBalance;

            using (BankingSystemContext context = new BankingSystemContext())
            {
                User user = context.Users.Attach(AuthenticationManager.GetCurrentUser());
                SavingAccount savingAccount = user.SavingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (savingAccount == null)
                {
                    throw new ArgumentException($"Account {accountNumber} does not exist");
                }
                
                savingAccount.AddInterest();
                context.SaveChanges();
                currentBalance = savingAccount.Balance;
            }

            return $"Interest added to account {accountNumber}, current balance = {currentBalance:f2}";
        }

        private string Exit()
        {
            Environment.Exit(0);

            return string.Empty;
        }

        private void ValidateUser(User user)
        {
            bool isValid = true;
            string errors = string.Empty;
            if (user == null)
            {
                errors = "User cannot be null";
                throw new ArgumentException(errors);
            }

            Regex usernameRegex = new Regex(@"^[a-zA-Z]+[a-zA-Z0-9]{2,}$");
            if (!usernameRegex.IsMatch(user.Username))
            {
                isValid = false;
                errors += "Invalid Username\n";
            }

            Regex passwordRegex = new Regex(@"^(?=[a-zA-Z0-9]*[A-Z])(?=[a-zA-Z0-9]*[a-z])(?=[a-zA-Z0-9]*[0-9])[a-zA-Z0-9]{6,}$");
            if (!passwordRegex.IsMatch(user.Password))
            {
                isValid = false;
                errors += "Invalid Password\n";
            }

            Regex emailRegex = new Regex(@"^([a-zA-Z0-9]+[-|_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[-]?)*[a-zA-Z0-9]+\.([a-zA-Z0-9]+[-]?)*[a-zA-Z0-9]+$");
            if (!emailRegex.IsMatch(user.Email))
            {
                isValid = false;
                errors += "Invalid Email\n";
            }

            if (!isValid)
            {
                throw new ArgumentException(errors.Trim());
            }
        }

        private void ValidateSavingAccount(SavingAccount account)
        {
            string errors = string.Empty;
            bool isValid = true;

            if (account == null)
            {
                errors = "Account cannot be null";
                throw new ArgumentException(errors);
            }

            if (account.AccountNumber.Length != 10)
            {
                isValid = false;
                errors += "Account number length should be exactly 10 symbols\n";
            }

            if (account.Balance < 0)
            {
                isValid = false;
                errors += "Account balance cannot be negative\n";
            }

            if (account.InterestRate < 0)
            {
                isValid = false;
                errors += "Account interest rate cannot be negative, even though it is right now ;)\n";
            }

            if (!isValid)
            {
                throw new ArgumentException(errors.Trim());
            }
        }

        private void ValidateCheckingAccount(CheckingAccount account)
        {
            string errors = string.Empty;
            bool isValid = true;

            if (account == null)
            {
                errors = "Account cannot be null";
                throw new ArgumentException(errors);
            }

            if (account.AccountNumber.Length != 10)
            {
                isValid = false;
                errors += "Account number length should be exactly 10 symbols\n";
            }

            if (account.Balance < 0)
            {
                isValid = false;
                errors += "Account balance cannot be negative\n";
            }

            if (account.Fee < 0)
            {
                isValid = false;
                errors += "Account fee cannot be negative\n";
            }

            if (!isValid)
            {
                throw new ArgumentException(errors.Trim());
            }
        }

    }
}
