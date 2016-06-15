using System;
using System.Collections.Generic;
using System.Linq;

namespace Luma.SmartHub.Audio.Playback
{
    public static class PlaylistHelper
    {
        public static IList<ITrackInfo> ToTrackInfos(this IList<Uri> tracks)
        {
            return tracks
                .Select(c => new TrackInfo { Uri = c })
                .Cast<ITrackInfo>()
                .ToList();
        }
    }
}
