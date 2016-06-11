using System;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlaylistProvider
    {
        Uri[] CreatePlaylist(Uri playlistUrl);
    }
}
