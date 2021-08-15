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
        Name = "motd",
        Description = "Manage the MOTD.",
        Permission = "bp.motd",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "motd <set/enable/disable/force> <Duration> <Text>"
        )]
    public class MOTDCommand : ISynapseCommand
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
                        case "enable":
                        case "on":
                            if (player.HasPermission("bp.motd.enable"))
                            {
                                if (Plugin.Config.MOTD.IsEnabled)
                                {
                                    result.Message = "The MOTD is already enabled!";
                                    result.State = CommandResultState.Error;
                                    return result;
                                }
                                else
                                {
                                    Plugin.Config.MOTD.IsEnabled = true;
                                    Server.Get.Configs.UpdateSection("BroadcastsPlus", Plugin.Config);
                                    result.Message = "The MOTD has been enabled!";
                                    result.State = CommandResultState.Ok;
                                    return result;
                                }
                            }
                            else
                            {
                                result.Message = "You are missing the permission bp.motd.enable!";
                                result.State = CommandResultState.Error;
                                return result;
                            }
                        case "disable":
                        case "off":
                            if (player.HasPermission("bp.motd.disable"))
                            {
                                if (!Plugin.Config.MOTD.IsEnabled)
                                {
                                    result.Message = "The MOTD is already disabled!";
                                    result.State = CommandResultState.Error;
                                    return result;
                                }
                                else
                                {
                                    Plugin.Config.MOTD.IsEnabled = false;
                                    Server.Get.Configs.UpdateSection("BroadcastsPlus", Plugin.Config);
                                    result.Message = "The MOTD has been disabled!";
                                    result.State = CommandResultState.Ok;
                                    return result;
                                }
                            }
                            else
                            {
                                result.Message = "You are missing the permission bp.motd.disable!";
                                result.State = CommandResultState.Error;
                                return result;
                            }
                        case "force":
                            if (player.HasPermission("bp.motd.force"))
                            {

                                    Map.Get.SendBroadcast(Plugin.Config.MOTD.Duration, Plugin.Config.MOTD.Text, true);
                                    result.Message = "The MOTD has been forced!";
                                    result.State = CommandResultState.Ok;
                                    return result;
                            }
                            else
                            {
                                result.Message = "You are missing the permission bp.motd.force!";
                                result.State = CommandResultState.Error;
                                return result;
                            }
                        default:
                            result.Message = "\n#############################\nMOTD Commands:" +
                                "\n- motd enable:\nEnable the MOTD System." +
                                "\n- motd disable\nDisable the MOTD System." +
                                "\n- motd set <Duration> <Text>\nChange the current MOTD." +
                                "\n- motd force\nShows the MOTD to everyone on the server." +
                                "\n#############################\n";
                            result.State = CommandResultState.Error;
                            return result;
                    }
                case 3:
                    switch (context.Arguments.Array.ElementAt(1))
                    {
                        case "set":
                            if(ushort.TryParse(context.Arguments.Array.ElementAt(2), out ushort duration))
                            {
                                string message = "";
                                for (int i = 3; i < context.Arguments.Count; i++)
                                    message = message + context.Arguments.Array[i + 1] + " ";
                                Plugin.Config.MOTD.Text = message;
                                Plugin.Config.MOTD.Duration = duration;
                                Server.Get.Configs.UpdateSection("BroadcastsPlus", Plugin.Config);
                                result.State = CommandResultState.Ok;
                                result.Message = "\n#############################\nMOTD successfully set!" +
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
                        default:
                            result.Message = "\n#############################\nMOTD Commands:" +
                                "\n- motd enable:\nEnable the MOTD System." +
                                "\n- motd disable\nDisable the MOTD System." +
                                "\n- motd set <Duration> <Text>\nChange the current MOTD." +
                                "\n- motd force\nShows the MOTD to everyone on the server." +
                                "\n#############################\n";
                            result.State = CommandResultState.Error;
                            return result;
                    }
                default:
                    result.Message = "\n#############################\nMOTD Commands:" +
                                "\n- motd enable:\nEnable the MOTD System." +
                                "\n- motd disable\nDisable the MOTD System." +
                                "\n- motd set <Duration> <Text>\nChange the current MOTD." +
                                "\n- motd force\nShows the MOTD to everyone on the server." +
                                "\n#############################\n";
                    result.State = CommandResultState.Error;
                    return result;


            }
        }
    }
}



