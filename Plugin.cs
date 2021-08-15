using Synapse.Api.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastsPlus
{
    [PluginInformation(
        Author = "TheVoidNebula",
        Description = "Adds Broadcasts which play over time.",
        LoadPriority = 0,
        Name = "BroadcastsPlus",
        SynapseMajor = 2,
        SynapseMinor = 6,
        SynapsePatch = 0,
        Version = "1.0"
        )]
    public class Plugin : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "BroadcastsPlus")]
        public static Config Config;
        public override void Load()
        {
            SynapseController.Server.Logger.Info("BroadcastsPlus loaded!");

            new EventHandlers();
        }

    }
}
