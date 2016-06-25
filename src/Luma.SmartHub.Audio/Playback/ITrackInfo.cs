using System;

namespace Luma.SmartHub.Audio.Playback
{
    public interface ITrackInfo
    {
        string Name { get; set; }
        string Artist { get; set; }
        Uri Uri { get; set; }
    }

    public class TrackInfo : ITrackInfo
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public Uri Uri { get; set; }
    }
}