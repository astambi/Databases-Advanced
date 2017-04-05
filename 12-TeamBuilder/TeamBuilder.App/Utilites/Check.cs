namespace TeamBuilder.App.Utilites
{
    using System;

    public static class Check
    {
        public static void CheckLength(int expectedLength, string[] args)
        {
            if (expectedLength != args.Length)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }
    }
}
