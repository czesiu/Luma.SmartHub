using System.Collections.Generic;
using Luma.SmartHub.Audio.Playback;

namespace Luma.SmartHub.Audio
{
    /// <summary>
    /// This is API responsible for handle user playbacks. Playlists and other.
    /// </summary>
    public interface IAudioPlayer
    {
        IAudioHub AudioHub { get; }
        IEnumerable<IPlayback> Playbacks { get; }
        void AddPlayback(IPlayback playback);
        void RemovePlayback(IPlayback playback);
    }
}
