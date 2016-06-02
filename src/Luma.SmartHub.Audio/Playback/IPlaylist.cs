using System.Collections.Generic;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlaylist : IPlayback
    {
        IList<IPlayback> Tracks { get; }
        IPlayback CurrentTrack { get; }
        void Next();
        void Prev();
    }
}