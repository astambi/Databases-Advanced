namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilites;

    class InviteToTeamCommand
    {
        // InviteToTeam <teamName> <username>
        public string Execute(string[] inputArgs)
        {
            // Validate arguments counts
            Check.CheckLength(2, inputArgs);

            // Authorize user
            AuthenticationManager.Authorize();

            User currentUser = AuthenticationManager.GetCurrentUser();

            // Get Arguments
            string teamName = inputArgs[0];
            string username = inputArgs[1];

            // Validate team & invited user
            if (!CommandHelper.IsTeamExisting(teamName) ||  // nonexisting team 
                !CommandHelper.IsUserExisting(username))    // nonexisting invited user 
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            // Validate current user as team creator or team member
            if (!CommandHelper.IsUserCreatorOfTeam(teamName, currentUser) &&    // neither team creator
                !CommandHelper.IsMemberOfTeam(teamName, currentUser.Username))  // nor team member
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            // Check if invited user is team member already
            if (CommandHelper.IsMemberOfTeam(teamName, username))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            // Check is invitation is already sent (same team, same invited user, is active)
            if (IsInvitationPending(teamName, username))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
            }

            // Send invitation to join team
            SendInvitation(teamName, username);

            return $"Team {teamName} invited {username}!";
        }

        private void SendInvitation(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User invitedUser = context.Users.FirstOrDefault(u => u.Username == username);
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                Invitation invitation = new Invitation()
                {
                    InvitedUserId = invitedUser.Id,
                    TeamId = team.Id
                    // new invitation is set as active in the constructor
                };

                // Check if invited user is team creator, 
                // If team creator => add invited user to team & make invitation inactive
                if (CommandHelper.IsUserCreatorOfTeam(teamName, invitedUser))
                {
                    team.Members.Add(invitedUser);  // non-membership already checked
                    invitation.IsActive = false;    // make invitation inactive
                }
                context.Invitations.Add(invitation);
                context.SaveChanges();
            }
        }

        private bool IsInvitationPending(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Invitations.Any(i => i.Team.Name == teamName &&
                                                    i.InvitedUser.Username == username &&
                                                    i.IsActive);
            }
        }
    }
}
