using System.Collections.Generic;
using System.Linq;

namespace Luma.SmartHub.Audio.Playback
{
    public static class PlaybackExtensions
    {
        public static void AddOutgoingConnections(this IPlayback playback, IEnumerable<IOutputAudioDevice> devices)
        {
            foreach (var device in devices)
            {
                playback.AddOutgoingConnection(device);
            }
        }

        public static void ClearOutgoingConnections(this IPlayback playback)
        {
            foreach (var audioDevice in playback.OutgoingConnections.ToArray())
            {
                playback.RemoveOutgoingConnection(audioDevice);
            }
        }
    }
}
