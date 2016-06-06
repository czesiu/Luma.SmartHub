using System;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlayback
    {
        string Id { get; }

        double Volume { get; set; }
        void Pause();
        void Play();
        void Stop();

        bool IsPlaying { get; }

        event EventHandler Ended;
        void AddOutgoingConnection(IAudioDevice audioDevice);
        void RemoveOutgoingConnection(IAudioDevice audioDevice);
    }
}