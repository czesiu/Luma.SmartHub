using System.Collections.Generic;

namespace Luma.SmartHub.Audio
{
    public interface IAudioHub
    {
        IList<IAudioDevice> Devices { get; }

        void Play(string url, IAudioDevice device = null);
    }
}