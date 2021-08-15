using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastsPlus
{
    public class Broadcast
    {
        public int ID { get; set; }

        public ushort Duration { get; set; }

        public string Text { get; set; }
    }
}
