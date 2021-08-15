using MEC;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastsPlus
{
    public class EventHandlers
    {
        public List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
            Server.Get.Events.Round.RoundEndEvent += OnRoundEnd;
            Server.Get.Events.Player.PlayerJoinEvent += OnJoin;
        }

        public void OnRoundStart()
        {
            if (Plugin.Config.EnableBroadcasts)
                Coroutines.Add(Timing.RunCoroutine(SendBroadcasts()));

            if (Plugin.Config.EnableCassieAnnouncements)
                Coroutines.Add(Timing.RunCoroutine(SendAnnouncements()));
        }

        public void OnRoundEnd()
        {
            foreach (CoroutineHandle x in Coroutines)
                Timing.KillCoroutines(x);
            Coroutines.Clear();
        }

        public void OnJoin(Synapse.Api.Events.SynapseEventArguments.PlayerJoinEventArgs ev)
        {
            if (Plugin.Config.MOTD.IsEnabled)
                ev.Player.SendBroadcast(Plugin.Config.MOTD.Duration, Plugin.Config.MOTD.Text, true);
        }

        public IEnumerator<float> SendBroadcasts()
        {
            while (true)
            {
                var x = Plugin.Config.Broadcasts[UnityEngine.Random.Range(0, Plugin.Config.Broadcasts.Count)];
                Map.Get.SendBroadcast(x.Duration, x.Text, true);
                yield return Timing.WaitForSeconds(Plugin.Config.BroadcastsIntervall);
            }
        }

        public IEnumerator<float> SendAnnouncements()
        {
            while (true)
            {
                var x = Plugin.Config.CassieAnnouncements[UnityEngine.Random.Range(0, Plugin.Config.CassieAnnouncements.Count)];
                Map.Get.Cassie(x.Text);
                yield return Timing.WaitForSeconds(Plugin.Config.CassieAnnouncementsIntervall);
            }
        }

    }
}
