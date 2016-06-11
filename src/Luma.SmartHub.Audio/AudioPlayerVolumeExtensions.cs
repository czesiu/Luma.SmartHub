using System;
using System.Collections.Generic;
using System.Linq;
using Luma.SmartHub.Audio.Playback;

namespace Luma.SmartHub.Audio
{
    /// <summary>
    /// Extended API for manipulating devices volume
    /// </summary>
    public static class AudioPlayerVolumeExtensions
    {
        /// <summary>
        /// Mute current volume by specific value
        /// </summary>
        /// <param name="audioPlayer"></param>
        /// <param name="volume">Percent mute value (from 0 to 1)</param>
        public static IDisposable Mute(this IAudioPlayer audioPlayer, double volume)
        {
            var volumesForPlaybacks = audioPlayer.Playbacks
                .Select(c => new PlaybackWithVolume
                {
                    Playback = c,
                    Volume = c.Volume
                });

            foreach (var playback in audioPlayer.Playbacks)
            {
                playback.Volume *= volume;
            }

            return new MuteDisposable(volumesForPlaybacks);
        }

        private class PlaybackWithVolume
        {
            internal IPlayback Playback { get; set; }
            internal double Volume { get; set; }
        }

        private class MuteDisposable : IDisposable
        {
            private readonly IEnumerable<PlaybackWithVolume> _volumesForPlaybacks;

            public MuteDisposable(IEnumerable<PlaybackWithVolume> volumesForPlaybacks)
            {
                if (volumesForPlaybacks == null)
                    throw new ArgumentNullException(nameof(volumesForPlaybacks));

                _volumesForPlaybacks = volumesForPlaybacks;
            }

            public void Dispose()
            {
                foreach (var volumesForPlayback in _volumesForPlaybacks)
                {
                    volumesForPlayback.Playback.Volume = volumesForPlayback.Volume;
                }
            }
        }
    }
}
