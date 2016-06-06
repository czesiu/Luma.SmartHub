using System;
using System.Linq;
using System.Threading.Tasks;
using Luma.SmartHub.Audio.Playback;

namespace Luma.SmartHub.Audio
{
    /// <summary>
    /// API for playing audio with async TPL
    /// </summary>
    public static class AudioHubAsyncExtensions
    {
        /// <summary>
        /// Creates new thread with playing audio file
        /// </summary>
        /// <returns></returns>
        public static Task Play(this IAudioHub audioHub, Uri uri)
        {
            var playback = audioHub.CreatePlayback(uri);

            //if (playback.Duration.HasValue)
            //    throw new Exception();

            playback.AddOutgoingConnections(audioHub.Devices.OfType<IOutputAudioDevice>());
            playback.Play();

            var taskCompletionSource = new TaskCompletionSource<object>();

            playback.Ended += (sender, args) =>
            {
                taskCompletionSource.SetResult(null);

                var disposable = playback as IDisposable;
                disposable?.Dispose();
            };

            return taskCompletionSource.Task;
        }
    }
}
