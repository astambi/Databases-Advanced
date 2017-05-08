namespace PlanetHunters.Data.Store
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using Utilities;

    public static class TelescopeStore
    {
        public static void AddTelescopes(IEnumerable<TelescopeDto> telescopes)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var telescope in telescopes)
                {
                    // Validate Name
                    if (telescope.Name == null || telescope.Name.Length > 255)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate Location
                    if (telescope.Location == null || telescope.Location.Length > 255)
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Validate MirrorDiameter
                    float mirrorDiameter;
                    bool IsNumberDiameter = float.TryParse(telescope.MirrorDiameter, out mirrorDiameter);
                    if (telescope.MirrorDiameter != null && (!IsNumberDiameter || mirrorDiameter <= 0))
                    {
                        Console.WriteLine(Notifications.ErrorMsg.InvalidFormat);
                        continue;
                    }

                    // Create Entity
                    AddEntityTelescope(context, telescope);

                    // Success notification
                    Console.WriteLine(string.Format(Notifications.SuccessMsg.RecordImported, telescope.Name));
                }

                context.SaveChanges();
            }
        }

        private static void AddEntityTelescope(PlanetHuntersContext context, TelescopeDto telescope)
        {
            Telescope telescopeEntity = new Telescope()
            {
                Name = telescope.Name,
                Location = telescope.Location,
                MirrorDiameter = null
            };

            if (telescope.MirrorDiameter != null)
            {
                telescopeEntity.MirrorDiameter = float.Parse(telescope.MirrorDiameter);
            }

            context.Telescopes.Add(telescopeEntity);
        }
    }
}
