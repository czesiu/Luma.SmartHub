using System;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IUriPlayback : IPlayback
    {
        Uri Uri { get; }
    }
}
