using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace AudioPlayerApp
{
    public partial class MainWindow : Window
    {
        private List<string> _playlist;
        private int _currentTrackIndex;
        private bool _isPlaying;
        private bool _isRepeating;
        private bool _isShuffling;
        private Thread _positionThread;
        private Thread _timeDisplayThread;

        public MainWindow()
        {
            InitializeComponent();
            _playlist = new List<string>();
            _isPlaying = false;
            _isRepeating = false;
            _isShuffling = false;
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string[] musicFiles = Directory.GetFiles(dialog.FileName, "*.*", SearchOption.AllDirectories)
                    .Where(file => file.EndsWith(".mp3") || file.EndsWith(".m4a") || file.EndsWith(".wav"))
                    .ToArray();

                if (musicFiles.Length > 0)
                {
                    _playlist.Clear();
                    _playlist.AddRange(musicFiles);
                    _currentTrackIndex = 0;
                    StartPlayback(_playlist[_currentTrackIndex]);
                }
            }
        }

        private void StartPlayback(string track)
        {
            mediaElement.Source = new Uri(track);
            mediaElement.Play();
            _isPlaying = true;
            // Start threads for position slider and time display
            _positionThread = new Thread(UpdatePositionSlider);
            _positionThread.Start();
            _timeDisplayThread = new Thread(UpdateTimeDisplay);
            _timeDisplayThread.Start();
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isPlaying)
            {
                mediaElement.Pause();
                _isPlaying = false;
            }
            else
            {
                mediaElement.Play();
                _isPlaying = true;
            }
        }

        private void NextTrackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isShuffling)
            {
                ShufflePlaylist();
            }
            else
            {
                _currentTrackIndex = (_currentTrackIndex + 1) % _playlist.Count;
            }
            StartPlayback(_playlist[_currentTrackIndex]);
        }

        private void PreviousTrackButton_Click(object sender, RoutedEventArgs e)
        {
            _currentTrackIndex = (_currentTrackIndex - 1 + _playlist.Count) % _playlist.Count;
            StartPlayback(_playlist[_currentTrackIndex]);
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            _isRepeating = !_isRepeating;
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            _isShuffling = !_isShuffling;
            if (_isShuffling)
            {
                ShufflePlaylist();
            }
            else
            {
                _playlist.Sort();
            }
        }

        private void ShufflePlaylist()
        {
            Random rng = new Random();
            int n = _playlist.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = _playlist[k];
                _playlist[k] = _playlist[n];
                _playlist[n] = value;
            }
        }

        private void UpdatePositionSlider()
        {
            while (mediaElement.NaturalDuration.HasTimeSpan && _isPlaying)
            {
                TimeSpan duration = mediaElement.NaturalDuration.TimeSpan;
                double totalSeconds = duration.TotalSeconds;
                double currentPosition = mediaElement.Position.TotalSeconds;
                Dispatcher.Invoke(() => positionSlider.Value = (currentPosition / totalSeconds) * 100);
                Thread.Sleep(500);
            }
        }

        private void UpdateTimeDisplay()
        {
            while (mediaElement.NaturalDuration.HasTimeSpan && _isPlaying)
            {
                TimeSpan duration = mediaElement.NaturalDuration.TimeSpan;
                TimeSpan remainingTime = duration - mediaElement.Position;
                Dispatcher.Invoke(() =>
                {
                    currentTimeLabel.Content = mediaElement.Position.ToString(@"mm\:ss");
                    remainingTimeLabel.Content = remainingTime.ToString(@"mm\:ss");
                });
                Thread.Sleep(1000);
            }
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow(_playlist);
            if (historyWindow.ShowDialog() == true)
            {
                _currentTrackIndex = historyWindow.SelectedTrackIndex;
                StartPlayback(_playlist[_currentTrackIndex]);
            }
        }
    }
}