using System;
using System.Collections.Generic;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlayback
    {
        string Id { get; }
        double Volume { get; set; }
        double Name { get; set; }
        bool IsPlaying { get; }
        IEnumerable<IOutputAudioDevice> OutgoingConnections { get; }

        void Pause();
        void Play();
        void Stop();

        event EventHandler Ended;
        void AddOutgoingConnection(IAudioDevice audioDevice);
        void RemoveOutgoingConnection(IAudioDevice audioDevice);
    }
}