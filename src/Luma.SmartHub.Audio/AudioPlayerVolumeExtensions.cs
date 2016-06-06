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
            var volumesForDevices = audioPlayer.Playbacks
                .Select(c => new PlaybackWithVolume
                {
                    Playback = c,
                    Volume = c.Volume
                });

            foreach (var audioDevice in audioPlayer.Playbacks)
            {
                audioDevice.Volume *= volume;
            }

            return new MuteDisposable(volumesForDevices);
        }

        private class PlaybackWithVolume
        {
            internal IPlayback Playback { get; set; }
            internal double Volume { get; set; }
        }

        private class MuteDisposable : IDisposable
        {
            private readonly IEnumerable<PlaybackWithVolume> _volumesForDevices;

            public MuteDisposable(IEnumerable<PlaybackWithVolume> volumesForDevices)
            {
                if (volumesForDevices == null)
                    throw new ArgumentNullException(nameof(volumesForDevices));

                _volumesForDevices = volumesForDevices;
            }

            public void Dispose()
            {
                foreach (var volumesForDevice in _volumesForDevices)
                {
                    volumesForDevice.Playback.Volume = volumesForDevice.Volume;
                }
            }
        }
    }
}
