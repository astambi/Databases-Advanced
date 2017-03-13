using System;

namespace _11_Banking_System.Utils
{
    public class AccountGenerator
    {
        public static string GenerateAccountNumber()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10).ToUpper();
        }
    }
}
