using System;
using System.Collections.Generic;
using System.Linq;

namespace Luma.SmartHub.Audio.Playback
{
    public class PlaylistPlayback : IPlaylistPlayback
    {
        private readonly IAudioHub _audioHub;
        private IPlayback _playback;
        private ITrackInfo _currentTrack;

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

        // TODO: Add valid implementation - this is temporary
        public double Volume
        {
            get { return _playback?.Volume ?? 0; }
            set { _playback.Volume = value; }
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

                var oldTrack = _currentTrack;

                _currentTrack = value;

                OnCurrentTrackChanged(oldTrack, _currentTrack);
            }
        }

        public PlaylistPlayback(IAudioHub audioHub, IList<Uri> tracks)
            : this(audioHub, tracks.ToTrackInfos()) { }

        public PlaylistPlayback(IAudioHub audioHub, IList<ITrackInfo> tracks = null)
        {
            _audioHub = audioHub;

            Id = Guid.NewGuid().ToString();
            Tracks = tracks ?? new List<ITrackInfo>();
            OutgoingConnections = new HashSet<IOutputAudioDevice>();
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
            _playback.Ended += OnCurrentTrackEnded;
            _playback.AddOutgoingConnections(OutgoingConnections);
        }

        private void OnCurrentTrackEnded(object sender, EventArgs eventArgs)
        {
            Next();
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
