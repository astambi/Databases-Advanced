using _05_Photographers_Albums.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace _05_Photographers_Albums
{
    class P05_PhotographersAlbums
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialiazing database [Photographers.CodeFirst]");
            PhotographersContext context = new PhotographersContext();
            context.Database.Initialize(true);

            Console.WriteLine("\nSolutions to Problems:\n" +
                " 5.  Photographers\n" +
                " 6.  Albums\n" +
                " 7.  Tags\n" +
                " 8. *Tag Attribute\n" +
                " 9.  Shared Albums\n" +
                "10.**Photographer Roles\n" +
                "\nRollback migrations to see previous versions of the database.\n");
            Console.WriteLine("*************************************************************");

            AddTagUsingTagTransform(context);          // Problem 8 TagTransform in the Setter            
            //AddTagUsingAttributeValidation(context); // Problem 8 TagTransform with Attribute Validation
        }

        private static void AddTagUsingTagTransform(PhotographersContext context)
        {
            /* Using TagTransform in the Setter, no atttibute validations. 
             * To test this method => 
             * Comment (Disable) the [Tag] annotation in Tag.cs & 
             * Uncomment (Enable) the respective anotated lines of code in Tag.cs */

            Console.WriteLine("Solution to Problem 8:\n" +
                "* Using TagTransform in the setter, no atttibute validation.\n" +
                "* To test TagTransform with attribute validation, follow the comments.");
            string tagName = String.Empty;
            while (true)
            {
                Console.Write("Enter Tag Name: ");
                tagName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(tagName)) break;
                Console.Write("Tag Name cannot be empty. ");
            }
            Tag tag = new Tag() { Name = tagName }; // a valid tag is set in the setter

            if (context.Tags.FirstOrDefault(t => t.Name == tag.Name) == null)
            {
                context.Tags.Add(tag);
                context.SaveChanges();
                Console.WriteLine($"{tag.Name} was added to the database");
            }
            else Console.WriteLine($"{tag.Name} already exists in the database");
        }

        private static void AddTagUsingAttributeValidation(PhotographersContext context)
        {
            /* Using TagTransform with TagAttribute Validations. 
             * To test this method => 
             * Uncomment (Enable) the [Tag] annotation in Tag.cs & 
             * Comment (Disdable) the respective anotated lines of code in Tag.cs */

            Console.WriteLine("Solution to Problem 8:\n" +
                "* Using TagTransform with attribute validation.");
            string tagName = String.Empty;
            while (true)
            {
                Console.Write("Enter Tag Name: ");
                tagName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(tagName)) break;
                Console.Write("Tag Name cannot be empty. ");
            }
            Tag tag = new Tag() { Name = tagName };
            context.Tags.Add(tag);

            try
            {
                context.SaveChanges();
                Console.WriteLine($"{tag.Name} was added to the database");
            }
            catch (DbEntityValidationException e)
            {
                tag.Name = TagTransformer.Transform(tag.Name);
                if (context.Tags.FirstOrDefault(t => t.Name == tag.Name) == null)
                {
                    context.SaveChanges();
                    Console.WriteLine($"{tag.Name} was added to the database");
                }
                else Console.WriteLine($"{tag.Name} already exists in the database");
            }
        }
    }
}
