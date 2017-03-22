using System;

namespace PhotoShare.Client.Utilities
{
    public class ValidateInput
    {
        public static void CheckExactInputArgsCount(string commandName, int argsCount, int requiredCount)
        {
            if (argsCount != requiredCount)
            {
                throw new InvalidOperationException($"Command {commandName} not valid!");
            }
        }

        public static void CheckMinInputArgsCount(string commandName, int argsCount, int requriedMinCount)
        {
            if (argsCount < requriedMinCount)
            {
                throw new InvalidOperationException($"Command {commandName} not valid!");
            }
        }
    }
}
