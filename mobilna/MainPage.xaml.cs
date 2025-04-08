using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace NF_04_2025_01_Music_Player
{
    public partial class MainPage : ContentPage
    {
        private List<Song> _songs;
        private int _currentSongIndex;

        /// ******************************************************
        /// nazwa funkcji: MainPage
        /// typ zwracany: brak, konstruktor klasy MainPage
        /// informacje: Inicjalizuje komponenty strony i ładuje dane utworów.
        /// autor: nic00la1
        /// ******************************************************
        public MainPage()
        {
            InitializeComponent();
            InitializeSongs();
            UpdateUI();
        }

        /// ******************************************************
        /// nazwa funkcji: InitializeSongs
        /// typ zwracany: void, brak zwracanej wartości
        /// informacje: Odczytuje dane utworów z pliku `music.txt` i inicjalizuje listę utworów.
        /// autor: nic00la1
        /// ******************************************************
        private void InitializeSongs()
        {
            _songs = new List<Song>();
            string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "music.txt");

            // Kopiowanie pliku z zasobów do katalogu aplikacji (jeśli jeszcze nie istnieje)
            if (!File.Exists(filePath))
            {
                using var stream = FileSystem.OpenAppPackageFileAsync("music.txt").Result;
                using var reader = new StreamReader(stream);
                File.WriteAllText(filePath, reader.ReadToEnd());
            }

            // Odczyt danych z pliku
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 4)
                {
                    _songs.Add(new Song
                    {
                        Author = parts[0],
                        Title = parts[1],
                        Duration = parts[2],
                        CoverImage = parts[3]
                    });
                }
            }

            _currentSongIndex = 0;
        }

        /// ******************************************************
        /// nazwa funkcji: UpdateUI
        /// typ zwracany: void, brak zwracanej wartości
        /// informacje: Aktualizuje interfejs użytkownika na podstawie bieżącego utworu.
        /// autor: nic00la1
        /// ******************************************************
        private void UpdateUI()
        {
            var currentSong = _songs[_currentSongIndex];
            SongTitle.Text = currentSong.Title;
            SongAuthor.Text = currentSong.Author;
            TotalTime.Text = currentSong.Duration;
            AlbumCover.Source = currentSong.CoverImage;
            CurrentTime.Text = "0:00";
            SongSlider.Value = 0;
        }

        /// ******************************************************
        /// nazwa funkcji: OnNextButtonClicked
        /// typ zwracany: void, brak zwracanej wartości
        /// informacje: Obsługuje kliknięcie przycisku "Next" i przechodzi do następnego utworu.
        /// autor: nic00la1
        /// ******************************************************
        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            _currentSongIndex = (_currentSongIndex + 1) % _songs.Count;
            UpdateUI();
        }

        /// ******************************************************
        /// nazwa funkcji: OnBackButtonClicked
        /// typ zwracany: void, brak zwracanej wartości
        /// informacje: Obsługuje kliknięcie przycisku "Back" i przechodzi do poprzedniego utworu.
        /// autor: nic00la1
        /// ******************************************************
        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            _currentSongIndex = (_currentSongIndex - 1 + _songs.Count) % _songs.Count;
            UpdateUI();
        }
    }
}