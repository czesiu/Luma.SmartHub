using System;
using System.Collections.Generic;
using Luma.SmartHub.Audio.Playback;

namespace Luma.SmartHub.Audio
{
    public interface IAudioHub
    {
        double Volume { get; set; }
        IList<IAudioDevice> Devices { get; }
        IPlayback CreatePlayback(Uri uri);
    }
}