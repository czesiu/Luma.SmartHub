using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Luma.SmartHub.Audio.Playback
{
    public class PlaylistPlayback : IPlaylistPlayback
    {
        private readonly IAudioHub _audioHub;
        private readonly IPlaybackInfoProvider _playbackInfoProvider;
        private IPlayback _playback;
        private ITrackInfo _currentTrack;

        private readonly object _lock = new object();

        protected readonly ILogger Logger = Log.ForContext<PlaylistPlayback>();

        public string Id { get; }

        public double? Duration => _playback?.Duration;

        public double Position
        {
            get { return _playback?.Position ?? 0; }
            set { _playback.Position = value; }
        }

        public string Name
        {
            get { return _playback?.Name; }
            set { _playback.Name = value; }
        }

        private double _volume = 1;
        public double Volume
        {
            get { return _volume; }
            set
            {
                _volume = Math.Max(0, value);

                OnVolumeChanged();
            }
        }

        public IList<ITrackInfo> Tracks { get; }

        public bool IsPlaying => _playback?.IsPlaying == true;

        ICollection<IOutputAudioDevice> OutgoingConnections { get; }

        IEnumerable<IOutputAudioDevice> IPlayback.OutgoingConnections => OutgoingConnections;

        public event EventHandler Ended;

        public ITrackInfo CurrentTrack
        {
            get { return _currentTrack; }
            set
            {
                if (_currentTrack == value)
                    return;

                lock (_lock)
                {
                    var oldTrack = _currentTrack;

                    _currentTrack = value;

                    OnCurrentTrackChanged(oldTrack, _currentTrack);
                }
            }
        }

        public PlaylistPlayback(IAudioHub audioHub, IPlaybackInfoProvider playbackInfoProvider, IList<Uri> tracks)
            : this(audioHub, playbackInfoProvider, tracks.ToTrackInfos()) { }

        public PlaylistPlayback(IAudioHub audioHub, IPlaybackInfoProvider playbackInfoProvider, IList<ITrackInfo> tracks = null)
        {
            _audioHub = audioHub;
            _playbackInfoProvider = playbackInfoProvider;

            Id = Guid.NewGuid().ToString();
            Tracks = tracks ?? new List<ITrackInfo>();
            OutgoingConnections = new HashSet<IOutputAudioDevice>();

            Task.Run(FillPlaybackInfos);
        }

        private Task FillPlaybackInfos()
        {
            var result = Parallel.ForEach(Tracks, trackInfo =>
            {
                try
                {
                    var playbackInfo = _playbackInfoProvider.Get(trackInfo.Uri);

                    if (playbackInfo == null)
                        return;

                    trackInfo.Name = playbackInfo.Name;
                    trackInfo.Uri = playbackInfo.Uri;

                    Logger.Debug("Playback info provider returned {playbackInfo}", playbackInfo);
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Error during getting playback info for {url}", trackInfo.Uri);

                    throw;
                }
            });

            return Task.FromResult(result);
        }

        public void Pause()
        {
            _playback?.Pause();
        }

        public void Play()
        {
            if (CurrentTrack == null)
            {
                CurrentTrack = Tracks.First();
            }

            _playback?.Play();
        }

        public void Stop()
        {
            _playback?.Stop();

            CurrentTrack = Tracks.First();
        }

        public void AddOutgoingConnection(IOutputAudioDevice audioDevice)
        {
            OutgoingConnections.Add(audioDevice);

            _playback?.AddOutgoingConnection(audioDevice);
        }

        public void RemoveOutgoingConnection(IOutputAudioDevice audioDevice)
        {
            OutgoingConnections.Remove(audioDevice);

            _playback?.RemoveOutgoingConnection(audioDevice);
        }

        public void Next()
        {
            var currentIndex = Tracks.IndexOf(CurrentTrack);
            GoToIndex(currentIndex + 1);
        }

        public void Prev()
        {
            var currentIndex = Tracks.IndexOf(CurrentTrack);
            GoToIndex(currentIndex - 1);
        }

        private void OnCurrentTrackChanged(ITrackInfo oldTrack, ITrackInfo newTrack)
        {
            if (_playback != null)
            {
                _playback.Ended -= OnCurrentTrackEnded;
                _playback.Dispose();
            }

            _playback = _audioHub.CreatePlayback(newTrack.Uri);
            _playback.Name = newTrack.Name;
            _playback.Volume = Volume;
            _playback.Ended += OnCurrentTrackEnded;
            _playback.AddOutgoingConnections(OutgoingConnections);
        }

        private void OnCurrentTrackEnded(object sender, EventArgs e)
        {
            Next();
        }

        private void OnVolumeChanged()
        {
            if (_playback == null)
                return;

            _playback.Volume = Volume;
        }

        private void GoToIndex(int index)
        {
            if (index > Tracks.Count)
                index = 0;

            if (index < 0)
                index = Tracks.Count - 1;

            CurrentTrack = Tracks[index];

            Play();
        }
    }
}
