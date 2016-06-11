using System;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlaybackInfoProvider
    {
        PlaybackInfo Get(Uri uri);
    }
}
