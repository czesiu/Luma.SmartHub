using System;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlaybackManager
    {
        PlaybackInfo TryGetPlaybackInfo(Uri uri);
    }
}
