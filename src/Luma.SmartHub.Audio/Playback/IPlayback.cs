namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlayback
    {
        string Id { get; }

        void Pause();
        void Play();
        void Stop();

        bool IsPlaying { get; }

        void AddOutgoingConnection(IAudioDevice audioDevice);
        void RemoveOutgoingConnection(IAudioDevice audioDevice);
    }
}