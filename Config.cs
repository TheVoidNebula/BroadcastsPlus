using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastsPlus
{
    public class Config : AbstractConfigSection
    {
        [Description("Should this plugin be enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should Broadcasts be posted in a given intervall?")]
        public bool EnableBroadcasts { get; set; } = true;

        [Description("The intervall in which the Broadcasts are posted")]
        public ushort BroadcastsIntervall { get; set; } = 60;

        [Description("Should C.A.S.S.I.E announcements be posted in a given intervall?")]
        public bool EnableCassieAnnouncements { get; set; } = false;

        [Description("The intervall in which the C.A.S.S.I.E announcements are posted")]
        public ushort CassieAnnouncementsIntervall { get; set; } = 180;

        [Description("The intervall in which the C.A.S.S.I.E announcements are posted")]
        public MOTD MOTD { get; set; } = new MOTD()
        {
            IsEnabled = true,
            Duration = 10,
            Text = "<color=green>Welcome to our Server!</color>\nWe are currently looking for staff!"
        };

        public List<Broadcast> Broadcasts = new List<Broadcast>()
        {
            new Broadcast()
            {
                ID = 1,
                Duration = 10,
                Text = "<b>This is a Test Broadcast!</b>"
            },
            new Broadcast()
            {
                ID = 2,
                Duration = 10,
                Text = "<b>Remember to read our rules!</b>"
            }
        };

        public List<CassieAnnouncement> CassieAnnouncements = new List<CassieAnnouncement>()
        {
            new CassieAnnouncement()
            {
                ID = 1,
                Text = "based"
            }
        };
    }


}
