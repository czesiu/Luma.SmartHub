using System.Collections.Generic;

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
    }
}
