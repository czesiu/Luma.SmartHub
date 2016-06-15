using System;

namespace Luma.SmartHub.Audio.Playback
{
    public interface ITrackInfo
    {
        string Name { get; }
        string Artist { get; }
        Uri Uri { get; }
    }

    public class TrackInfo : ITrackInfo
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public Uri Uri { get; set; }
    }
}