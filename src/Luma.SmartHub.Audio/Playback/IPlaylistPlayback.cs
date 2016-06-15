using System.Collections.Generic;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlaylistPlayback : IPlayback
    {
        IList<ITrackInfo> Tracks { get; }
        ITrackInfo CurrentTrack { get; }
        void Next();
        void Prev();
    }
}