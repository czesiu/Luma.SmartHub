using System.Collections.Generic;
using Luma.SmartHub.Audio.Playback;

namespace Luma.SmartHub.Audio
{
    /// <summary>
    /// This is API responsible for handle user playbacks. Playlists and other.
    /// </summary>
    public interface IAudioPlayer
    {
        IEnumerable<IPlayback> Playbacks { get; }
    }
}
