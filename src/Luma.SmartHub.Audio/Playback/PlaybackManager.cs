using System;

namespace Luma.SmartHub.Audio.Playback
{
    public class PlaybackManager : IPlaybackManager
    {
        private readonly IPlaybackInfoProvider[] _playbackInfoProviders;

        public PlaybackManager(IPlaybackInfoProvider[] playbackInfoProviders)
        {
            _playbackInfoProviders = playbackInfoProviders;
        }

        public PlaybackInfo TryGetPlaybackInfo(Uri uri)
        {
            foreach (var playbackInfoProvider in _playbackInfoProviders)
            {
                var playbackInfo = playbackInfoProvider.Get(uri);

                if (playbackInfo != null)
                {
                    return playbackInfo;
                }
            }

            return null;
        }
    }
}
