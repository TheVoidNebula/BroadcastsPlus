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
        Name = "broadcasts",
        Aliases = new[] { "bcs" },
        Description = "Manage Broadcasts directly.",
        Permission = "bp.broadcasts",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = ".broadcasts <list/add/remove/reset/force> <ID> <Text>"
        )]
    public class BroadcastCommand : ISynapseCommand
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
                            if (player.HasPermission("bp.broadcasts.list"))
                            {
                                StringBuilder builder = new StringBuilder();
                                builder.Append("\n#############################\nBroadcasts:\n");
                                foreach (var Broadcasts in Plugin.Config.Broadcasts)
                                    builder.Append($"[{Broadcasts.ID}] '{Broadcasts.Text}' for {Broadcasts.Duration}s\n");
                                builder.Append("#############################\n");
                                result.Message = builder.ToString();
                                result.State = CommandResultState.Ok;
                                return result;
                            } 
                            else
                            {
                                result.Message = "You are missing the permission bp.broadcasts.list!";
                                result.State = CommandResultState.NoPermission;
                                return result;
                            }
                        case "reset":
                            if (player.HasPermission("bp.broadcasts.reset"))
                            {
                                Server.Get.Configs.UpdateSection("BroadcastsPlus", new Config());
                                result.Message = "The BroadcastsPlus Config has been reset!";
                                result.State = CommandResultState.Ok;
                                return result;
                            } 
                            else
                            {
                                result.Message = "You are missing the permission bp.broadcasts.reset!";
                                result.State = CommandResultState.NoPermission;
                                return result;
                            }
                        default:
                            result.State = CommandResultState.Error;
                            result.Message = "\n#############################\nBroadcasts Commands:" +
                                "\n- broadcasts list:\nShows all the current broadcasts." +
                                "\n- broadcasts add <ID> <Duration> <Text>\nCreate a new Broadcast." +
                                "\n- broadcasts remove <ID>\nDelete a broadcast via the ID." +
                                "\n- broadcasts reset\nResets the Config." +
                                "\n- broadcasts force <ID>\nForces a Broadcast to appear." +
                                "\n#############################\n";
                            return result;
                    }
                case 2:
                    switch (context.Arguments.Array.ElementAt(1))
                    {
                        case "remove":
                        case "delete":
                            if (player.HasPermission("bp.broadcasts.remove"))
                            {
                                if (int.TryParse(context.Arguments.Array.ElementAt(2), out int n))
                                {
                                    int x = 0;
                                    foreach (var Broadcasts in Plugin.Config.Broadcasts)
                                    {
                                        if (n == Broadcasts.ID)
                                        {
                                            x++;
                                            Plugin.Config.Broadcasts.Remove(Broadcasts);
                                            Server.Get.Configs.UpdateSection("BroadcastsPlus", Plugin.Config);
                                            result.State = CommandResultState.Ok;
                                            result.Message = $"The Broadcast with the ID '{Broadcasts.ID}' has been deleted!";
                                            return result;
                                        }
                                    }
                                    if (x == 0)
                                        result.Message = $"There is no Broadcast with the ID '{n}'!";
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
                                result.Message = "You are missing the permission bp.broadcasts.remove!";
                                result.State = CommandResultState.NoPermission;
                                return result;
                            }
                        case "force":
                            if (player.HasPermission("bp.broadcasts.force"))
                            {
                                if (int.TryParse(context.Arguments.Array.ElementAt(2), out int n2))
                                {
                                    int x = 0;
                                    foreach (var Broadcasts in Plugin.Config.Broadcasts)
                                    {
                                        if (x == Broadcasts.ID)
                                        {
                                            x++;
                                            Map.Get.SendBroadcast(Broadcasts.Duration, Broadcasts.Text, true);
                                            result.State = CommandResultState.Ok;
                                            result.Message = $"The Broadcast with the ID '{Broadcasts.ID}' has been forced!";
                                            return result;
                                        }
                                    }
                                    if (x == 0)
                                        result.Message = $"There is no Broadcast with the ID '{n2}'!";
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
                                result.Message = "You are missing the permission bp.broadcasts.force!";
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
                                        foreach (var Broadcasts in Plugin.Config.Broadcasts)
                                        {
                                            if (id == Broadcasts.ID)
                                            {
                                                result.State = CommandResultState.Error;
                                                result.Message = $"A Broadcast with the ID '{id}' already exists!";
                                                return result;
                                            }

                                        }
                                        if (ushort.TryParse(context.Arguments.Array.ElementAt(3), out ushort duration))
                                        {
                                            string message = "";
                                            for (int i = 3; i < context.Arguments.Count; i++)
                                                message = message + context.Arguments.Array[i + 1] + " ";
                                            Plugin.Config.Broadcasts.Add(new Broadcast()
                                            {
                                                Duration = duration,
                                                ID = id,
                                                Text = message
                                            });
                                            Server.Get.Configs.UpdateSection("BroadcastsPlus", Plugin.Config);
                                            result.State = CommandResultState.Ok;
                                            result.Message = "\n#############################\nBroadcast successfully created!" +
                                        $"\nID: {id}" +
                                        $"\nDuration: {duration}" +
                                        $"\nText: '{message}'" +
                                        "\n#############################\n";
                                            return result;
                                        }
                                        else
                                        {
                                            result.State = CommandResultState.Error;
                                            result.Message = "Duration has to be a number!";
                                            return result;
                                        }
                                    }
                                    else
                                    {
                                        result.State = CommandResultState.Error;
                                        result.Message = "ID has to be a number!";
                                        return result;
                                    }
                                default:
                                    result.State = CommandResultState.Error;
                                    result.Message = "\n#############################\nBroadcasts Commands:" +
                                "\n- broadcasts list:\nShows all the current broadcasts." +
                                "\n- broadcasts add <ID> <Duration> <Text>\nCreate a new Broadcast." +
                                "\n- broadcasts remove <ID>\nDelete a broadcast via the ID." +
                                "\n- broadcasts reset\nResets the Config." +
                                "\n- broadcasts force <ID>\nForces a Broadcast to appear." +
                                "\n#############################\n";
                                    return result;
                            }
            }
        }
    }
}

    

