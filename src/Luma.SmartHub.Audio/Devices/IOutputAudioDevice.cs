namespace Luma.SmartHub.Audio
{
    public interface IOutputAudioDevice : IAudioDevice
    {
        double Volume { get; set; }
    }
}