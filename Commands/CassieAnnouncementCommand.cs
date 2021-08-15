using Synapse;
using Synapse.Api;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastsPlus.Commands
{
    [CommandInformation(
        Name = "cassieannouncement",
        Aliases = new[] { "ca", "announcements" },
        Description = "Manage C.A.S.S.I.E announcements directly.",
        Permission = "bp.announcements",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = ".cassie <list/add/remove/reset/force> <ID> <Text>"
        )]
    public class CassieAnnouncementCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Player player = context.Player;


            switch (context.Arguments.Count)
            {
                case 1:
                    switch (context.Arguments.Array.ElementAt(1))
                    {
                        case "list":
                            if (player.HasPermission("bp.announcements.list"))
                            {
                                StringBuilder builder = new StringBuilder();
                                builder.Append("\n#############################\nAnnouncements:\n");
                                foreach (var Announcements in Plugin.Config.CassieAnnouncements)
                                    builder.Append($"[{Announcements.ID}] '{Announcements.Text}'\n");
                                builder.Append("#############################\n");
                                result.Message = builder.ToString();
                                result.State = CommandResultState.Ok;
                                return result;
                            } 
                            else
                            {
                                result.Message = "You are missing the permission bp.announcements.list!";
                                result.State = CommandResultState.NoPermission;
                                return result;
                            }
                        case "reset":
                            if (player.HasPermission("bp.announcements.reset"))
                            {
                                Server.Get.Configs.UpdateSection("BroadcastsPlus", new Config());
                                result.Message = "The BroadcastsPlus Config has been reset!";
                                result.State = CommandResultState.Ok;
                                return result;
                            } 
                            else
                            {
                                result.Message = "You are missing the permission bp.announcements.reset!";
                                result.State = CommandResultState.NoPermission;
                                return result;
                            }
                        default:
                            result.State = CommandResultState.Error;
                            result.Message = "\n#############################\nC.A.S.S.I.E Announcement Commands:" +
                                "\n- announcements list:\nShows all the current announcements." +
                                "\n- announcements add <ID><Text>\nCreate a new Announcement." +
                                "\n- announcements remove <ID>\nDelete a Announcement via the ID." +
                                "\n- announcements reset\nResets the Config." +
                                "\n- announcements force <ID>\nForces a Announcement to appear." +
                                "\n#############################\n";
                            return result;
                    }
                case 2:
                    switch (context.Arguments.Array.ElementAt(1))
                    {
                        case "remove":
                        case "delete":
                            if (player.HasPermission("bp.announcements.remove"))
                            {
                                if (int.TryParse(context.Arguments.Array.ElementAt(2), out int n))
                                {
                                    int x = 0;
                                    foreach (var Announcements in Plugin.Config.CassieAnnouncements)
                                    {
                                        if (n == Announcements.ID)
                                        {
                                            x++;
                                            Plugin.Config.CassieAnnouncements.Remove(Announcements);
                                            Server.Get.Configs.UpdateSection("BroadcastsPlus", Plugin.Config);
                                            result.State = CommandResultState.Ok;
                                            result.Message = $"The Announcement with the ID '{Announcements.ID}' has been deleted!";
                                            return result;
                                        }
                                    }
                                    if (x == 0)
                                        result.Message = $"There is no Announcement with the ID '{n}'!";
                                    return result;
                                }
                                else
                                {
                                    result.State = CommandResultState.Error;
                                    result.Message = "ID has to be a number!";
                                    return result;
                                }
                            }
                            else
                            {
                                result.Message = "You are missing the permission bp.announcements.remove!";
                                result.State = CommandResultState.NoPermission;
                                return result;
                            }
                        case "force":
                            if (player.HasPermission("bp.announcements.force"))
                            {
                                if (int.TryParse(context.Arguments.Array.ElementAt(2), out int n2))
                                {
                                    int x = 0;
                                    foreach (var Announcements in Plugin.Config.CassieAnnouncements)
                                    {
                                        if (x == Announcements.ID)
                                        {
                                            x++;
                                            Map.Get.Cassie( Announcements.Text, false, false);
                                            result.State = CommandResultState.Ok;
                                            result.Message = $"The Announcement with the ID '{Announcements.ID}' has been forced!";
                                            return result;
                                        }
                                    }
                                    if (x == 0)
                                        result.Message = $"There is no Announcement with the ID '{n2}'!";
                                    return result;
                                }
                                else
                                {
                                    result.State = CommandResultState.Error;
                                    result.Message = "ID has to be a number!";
                                    return result;
                                }
                            }
                            else
                            {
                                result.Message = "You are missing the permission bp.announcements.force!";
                                result.State = CommandResultState.NoPermission;
                                return result;
                            }
                    }
                    return result;
                            default:
                            switch (context.Arguments.Array.ElementAt(1))
                            {
                                case "add":
                                    if (int.TryParse(context.Arguments.Array.ElementAt(2), out int id))
                                    {
                                        foreach (var Announcements in Plugin.Config.CassieAnnouncements)
                                        {
                                            if (id == Announcements.ID)
                                            {
                                                result.State = CommandResultState.Error;
                                                result.Message = $"A Announcement with the ID '{id}' already exists!";
                                                return result;
                                            }

                                        }
                                            string message = "";
                                            for (int i = 2; i < context.Arguments.Count; i++)
                                                message = message + context.Arguments.Array[i + 1] + " ";
                                            Plugin.Config.CassieAnnouncements.Add(new CassieAnnouncement()
                                            {
                                                ID = id,
                                                Text = message
                                            });
                                            Server.Get.Configs.UpdateSection("BroadcastsPlus", Plugin.Config);
                                            result.State = CommandResultState.Ok;
                                            result.Message = "\n#############################\nAnnouncement successfully created!" +
                                        $"\nID: {id}" +
                                        $"\nText: '{message}'" +
                                        "\n#############################\n";
                                            return result;
                                        
 
                                    }
                                    else
                                    {
                                        result.State = CommandResultState.Error;
                                        result.Message = "ID has to be a number!";
                                        return result;
                                    }
                                default:
                            result.State = CommandResultState.Error;
                            result.Message = "\n#############################\nC.A.S.S.I.E Announcement Commands:" +
                        "\n- announcements list:\nShows all the current announcements." +
                        "\n- announcements add <ID><Text>\nCreate a new Announcement." +
                        "\n- announcements remove <ID>\nDelete a Announcement via the ID." +
                        "\n- announcements reset\nResets the Config." +
                        "\n- announcements force <ID>\nForces a Announcement to appear." +
                        "\n#############################\n";
                            return result;
                            }
                case 0:
                    result.State = CommandResultState.Error;
                    result.Message = "\n#############################\nC.A.S.S.I.E Announcement Commands:" +
                "\n- announcements list:\nShows all the current announcements." +
                "\n- announcements add <ID><Text>\nCreate a new Announcement." +
                "\n- announcements remove <ID>\nDelete a Announcement via the ID." +
                "\n- announcements reset\nResets the Config." +
                "\n- announcements force <ID>\nForces a Announcement to appear." +
                "\n#############################\n";
                    return result;
            }
        }
    }
}

    

