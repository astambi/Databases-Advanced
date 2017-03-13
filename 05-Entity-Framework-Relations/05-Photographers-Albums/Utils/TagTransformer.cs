using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _05_Photographers_Albums.Models
{
    public static class TagTransformer // Problem 8
    {
        public static string Transform(string tag)
        {
            string validTag = new Regex(@"\s+").Replace(tag.Trim(), String.Empty);

            if (!validTag.StartsWith("#"))
                validTag = "#" + validTag;

            if (validTag.Length > 20)
                validTag = validTag.Substring(0, 20);

            return validTag;
        }
    }
}
