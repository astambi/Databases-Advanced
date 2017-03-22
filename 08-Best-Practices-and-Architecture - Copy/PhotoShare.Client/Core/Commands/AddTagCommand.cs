namespace PhotoShare.Client.Core.Commands
{

    using Utilities;

    public class AddTagCommand
    {
        // AddTag <tag>
        public string Execute(string[] data)
        {
            string tag = data[1].ValidateOrTransform();

            //using (PhotoShareContext context = new PhotoShareContext())
            //{
            //    context.Tags.Add(new Tag
            //    {
            //        Name = tag
            //    });

            //    context.SaveChanges();
            //}

            //return tag + " was added successfully to database!";

            return null;
        }
    }
}
