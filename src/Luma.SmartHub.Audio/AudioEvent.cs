using System;
using System.Threading.Tasks;

namespace Luma.SmartHub.Audio
{
    [EventConfiguration(typeof(AudioEventConfiguration))]
    public class AudioEvent : IEvent
    {
        public static bool IsPlaying { get; set; }

        AudioEventConfiguration Configuration { get; }

        IAudioHub AudioHub { get; }
        IAudioPlayer AudioPlayer { get; }

        public AudioEvent(
            AudioEventConfiguration configuration,
            IAudioHub audioHub,
            IAudioPlayer audioPlayer
        )
        {
            Configuration = configuration;
            AudioHub = audioHub;
            AudioPlayer = audioPlayer;
        }

        public async Task Raise(IEventArgs args)
        {
            if (IsPlaying)
                return;

            try
            {
                IsPlaying = true;

                using (AudioPlayer.Mute(Configuration.MuteAudioLevel))
                {
                    await AudioHub.Play(Configuration.AudioUri);
                }
            }
            finally
            {
                IsPlaying = false;
            }
        }
    }

    public class AudioEventConfiguration
    {
        public double MuteAudioLevel { get; set; }
        public Uri AudioUri { get; set; }
    }
}
