using System.Linq;

namespace Luma.SmartHub.Audio
{
    public static class AudioHubDevicesExtensions
    {
        public static IOutputAudioDevice[] DefaultOutputs(this IAudioHub audioHub)
        {
            // mow all inputs is default, because there is no default flag in API
            return audioHub.Outputs();
        }

        public static IOutputAudioDevice[] Outputs(this IAudioHub audioHub)
        {
            return audioHub.Devices
                .OfType<IOutputAudioDevice>()
                .ToArray();
        }
    }
}
