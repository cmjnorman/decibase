using Decibase_Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Decibase_View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CRUDManager cm = new CRUDManager();

        public MainWindow()
        {
            InitializeComponent();
            RefreshAllLists();
        }

        public void RefreshAllLists()
        {
            ShowAllArtists();
            ShowAllAlbums();
            ShowAllTracks();
            ArtistData.Visibility = Visibility.Hidden;
            AlbumData.Visibility = Visibility.Hidden;
            TrackData.Visibility = Visibility.Hidden;
        }

        public void ShowAllArtists()
        {
            var artists = cm.RetrieveAllArtistNames();
            artists.Sort();
            ListBoxArtists.ItemsSource = artists;
        }

        public void ShowAllAlbums()
        {
            var albums = cm.RetrieveAllAlbumTitles();
            albums.Sort();
            ListBoxAlbums.ItemsSource = albums;
        }
        public void ShowArtistAlbums(string artistName)
        {
            var albums = cm.RetrieveArtistAlbumTitles(artistName);
            albums.Sort();
            ListBoxAlbums.ItemsSource = albums;
        }

        public void ShowAllTracks()
        {
            var tracks = cm.RetrieveAllTrackTitles();
            tracks.Sort();
            ListBoxTracks.ItemsSource = tracks;
        }

        public void ShowArtistTracks(string artistName)
        {
            var tracks = cm.RetrieveArtistTrackTitles(artistName);
            tracks.Sort();
            ListBoxTracks.ItemsSource = tracks;
        }

        public void ShowAlbumTracks(string albumTitle)
        {
            var tracks = cm.RetrieveAlbumTrackTitles(albumTitle);
            tracks.Sort();
            ListBoxTracks.ItemsSource = tracks;
        }

        public void DisplayArtistInformation(string artistName)
        {
            ArtistData.Visibility = Visibility.Visible;
            AlbumData.Visibility = Visibility.Hidden;
            TrackData.Visibility = Visibility.Hidden;
            DataArtist.Content = artistName;
        }

        public void DisplayAlbumInformation(string albumTitle)
        {
            ArtistData.Visibility = Visibility.Visible;
            AlbumData.Visibility = Visibility.Visible;
            TrackData.Visibility = Visibility.Hidden;
            DataArtist.Content = cm.ListToString(cm.RetrieveAlbumArtistNames(albumTitle));
            DataAlbum.Content = albumTitle;
            DataYear.Content = cm.GetAlbumYear(albumTitle);
        }

        public void DisplayTrackInformation(string trackTitle)
        {
            ArtistData.Visibility = Visibility.Visible;
            AlbumData.Visibility = Visibility.Visible;
            TrackData.Visibility = Visibility.Visible;
            DataArtist.Content = cm.ListToString(cm.RetrieveTrackArtistNames(trackTitle));
            DataAlbum.Content = cm.GetTrackAlbum(trackTitle);
            DataYear.Content = cm.GetAlbumYear(cm.GetTrackAlbum(trackTitle));
            DataTitle.Content = trackTitle;
            DataDisc.Content = $"{cm.GetDiscNumber(trackTitle)} / {cm.GetTotalDiscs(cm.GetTrackAlbum(trackTitle))}";
            DataTrack.Content = $"{cm.GetTrackNumber(trackTitle)} / {cm.GetTotalTracks(cm.GetTrackAlbum(trackTitle))}";
            DataGenre.Content = cm.GetTrackGenre(trackTitle);
        }

        public void SelectArtist(string artistName)
        {
            int artistIndex = -1;
            for (int i = 0; i < ListBoxArtists.Items.Count; i++)
            {
                if (ListBoxArtists.Items[i].ToString() == artistName)
                {
                    artistIndex = i;
                    break;
                }
            }
            ListBoxArtists.SelectedIndex = artistIndex;
            ShowArtistAlbums(artistName);
            ShowArtistTracks(artistName);
            DisplayArtistInformation(artistName);
        }

        public void SelectAlbum(string albumTitle)
        {
            int albumIndex = -1;
            for(int i = 0; i < ListBoxAlbums.Items.Count; i++)
            {
                if (ListBoxAlbums.Items[i].ToString() == albumTitle)
                {
                    albumIndex = i;
                    break;
                }
            }
            ListBoxAlbums.SelectedIndex = albumIndex;;
            ShowAlbumTracks(albumTitle);
            DisplayAlbumInformation(albumTitle);
        }

        public void SelectTrack(string trackTitle)
        {
            int trackIndex = -1;
            for (int i = 0; i < ListBoxTracks.Items.Count; i++)
            {
                if (ListBoxTracks.Items[i].ToString() == trackTitle)
                {
                    trackIndex = i;
                    break;
                }
            }
            ListBoxTracks.SelectedIndex = trackIndex;
            DisplayTrackInformation(trackTitle);
        }

        private void ListBoxArtists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxArtists.SelectedItem != null)
            {
                var artistName = ListBoxArtists.SelectedItem.ToString();
                cm.SelectItem(artistName, "artist");
                ShowArtistAlbums(artistName);
                ShowArtistTracks(artistName);
                DisplayArtistInformation(artistName);
            }
        }

        private void ListBoxAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxAlbums.SelectedItem != null)
            {
                var albumTitle = ListBoxAlbums.SelectedItem.ToString();
                cm.SelectItem(albumTitle, "album");
                ShowAlbumTracks(albumTitle);
                DisplayAlbumInformation(albumTitle);
            }

        }

        private void ListBoxTracks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxTracks.SelectedItem != null)
            {
                var trackTitle = ListBoxTracks.SelectedItem.ToString();
                cm.SelectItem(trackTitle, "track");
                DisplayTrackInformation(trackTitle);
            }
        }

        private void ButtonUnselect_Click(object sender, RoutedEventArgs e)
        {
            RefreshAllLists();
            cm.SelectedArtist = "";
            cm.SelectedAlbum = "";
            cm.SelectedTrack = "";
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var td = new TrackDialog();
            td.Show();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            cm.DeleteSelection();
            RefreshAllLists();
            if (cm.SelectedArtist != "") SelectArtist(cm.SelectedArtist);
            if (cm.SelectedAlbum != "") SelectAlbum(cm.SelectedAlbum);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if(cm.SelectedTrack != "")
            {
                var track = cm.SelectedTrack;
                var album = cm.GetTrackAlbum(track);
                var artists = cm.ListToString(cm.RetrieveTrackArtistNames(track));
                var td = new TrackDialog(track, album, artists, cm.GetDiscNumber(track), cm.GetTotalDiscs(album), cm.GetTrackNumber(track), cm.GetTotalTracks(album), cm.GetTrackGenre(track), cm.GetAlbumYear(album));
                td.Show();
            }
            else if (cm.SelectedAlbum != "")
            {
                var album = cm.SelectedAlbum;
                var ad = new AlbumDialog(album, cm.GetAlbumYear(album), cm.GetTotalDiscs(album), cm.GetTotalTracks(album));
                ad.Show();
            }
            else if (cm.SelectedArtist != "")
            {
                var artist = cm.SelectedArtist;
                var ad = new ArtistDialog(artist);
                ad.Show();
            }
        }
    }
}
