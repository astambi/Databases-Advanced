namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;
    using Utilities;

    public class AddTagCommand
    {
        private readonly TagService tagService;

        public AddTagCommand(TagService tagService)
        {
            this.tagService = tagService;
        }

        // AddTag <tag>
        public string Execute(string[] data)
        {
            string tag = data[0].ValidateOrTransform();

            // 2. Extend Photo Share System
            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials! You shoulld log in first.");
            }

            if (tagService.IsExistingTag(tag))
            {
                throw new ArgumentException($"Tag {tag} exists!");
            }

            tagService.AddTag(tag);

            return $"Tag {tag} was added successfully!";
        }
    }
}
