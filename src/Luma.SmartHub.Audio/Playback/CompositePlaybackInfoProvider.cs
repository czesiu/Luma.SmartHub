using System;

namespace Luma.SmartHub.Audio.Playback
{
    public class CompositePlaybackInfoProvider : IPlaybackInfoProvider
    {
        private readonly IPlaybackInfoProvider[] _playbackInfoProviders;

        public CompositePlaybackInfoProvider(IPlaybackInfoProvider[] playbackInfoProviders)
        {
            _playbackInfoProviders = playbackInfoProviders;
        }

        public PlaybackInfo Get(Uri uri)
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
