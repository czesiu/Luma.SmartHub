using System;

namespace Luma.SmartHub.Audio.Playback
{
    public interface IPlaybackUriTransformer
    {
        Uri Transform(Uri uri);
    }
}
