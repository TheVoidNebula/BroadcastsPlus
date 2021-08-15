using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastsPlus
{
    public class MOTD
    {
        public bool IsEnabled { get; set; }

        public ushort Duration { get; set; }

        public string Text { get; set; }
    }
}
