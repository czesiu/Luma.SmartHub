using System;
using System.Collections.Generic;
using Luma.SmartHub.Audio.Playback;

namespace Luma.SmartHub.Audio
{
    public static class AudioPlayerPlayingExtensions
    {
        public static IPlayback Play(this IAudioPlayer audioPlayer, Uri uri, IEnumerable<IOutputAudioDevice> devices = null)
        {
            devices = devices ?? audioPlayer.AudioHub.DefaultOutputs();

            var playback = audioPlayer.AudioHub.CreatePlayback(uri);
            
            playback.AddOutgoingConnections(devices);
            playback.Play();

            audioPlayer.AddPlayback(playback);

            return playback;
        }
    }
}