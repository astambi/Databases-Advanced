using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _05_Photographers_Albums.Attributes
{
    public class TagAttribute : ValidationAttribute // Problem 8 Using TagAttribute Validation
    {
        public override bool IsValid(object tagValue) // incl. int, string, etc.
        {
            string tagName = (string)tagValue;
            if (!Regex.IsMatch(tagName, @"^#\S{1,19}$"))
            {
                return false;
            }
            return true;
        }
    }
}