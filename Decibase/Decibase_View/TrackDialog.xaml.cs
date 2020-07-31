using Decibase_Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Decibase_View
{
    /// <summary>
    /// Interaction logic for TrackDialog.xaml
    /// </summary>
    public partial class TrackDialog : Window
    {
        private bool isNew = true;
        private string originalTitle;

        public TrackDialog()
        {
            InitializeComponent();
        }

        public TrackDialog(string title, string album, string artistString, string discNumber = "", string totalDiscs = "", string trackNumber = "", string totalTracks = "", string genre = "", string year = "")
        {
            InitializeComponent();
            TextBoxTitle.Text = title;
            originalTitle = title;
            TextBoxDisc.Text = discNumber;
            TextBoxTotalDiscs.Text = totalDiscs;
            TextBoxTrack.Text = trackNumber;
            TextBoxTotalTracks.Text = totalTracks;
            TextBoxGenre.Text = genre;
            TextBoxAlbum.Text = album;
            TextBoxYear.Text = year;
            TextBlockArtists.Text = artistString;
            isNew = false;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var sad = new SelectArtistDialog();
            sad.Owner = this;
            sad.Show();
        }

        public void AddArtist(string artistName)
        {
            if (TextBlockArtists.Text == "") TextBlockArtists.Text = artistName;
            else TextBlockArtists.Text += $"; {artistName}";
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlockArtists.Text != "")
            {
                var semiColonIndex = TextBlockArtists.Text.LastIndexOf(";");
                if (semiColonIndex == -1)
                { 
                    TextBlockArtists.Text = "";
                }
                else TextBlockArtists.Text = TextBlockArtists.Text.Remove(semiColonIndex);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxTitle.Text != "" && TextBoxAlbum.Text != "" && TextBlockArtists.Text != "")
            {
                if (isNew)
                {
                    ((MainWindow)Application.Current.MainWindow).cm.AddNewTrack(TextBoxTitle.Text, TextBoxAlbum.Text);
                    originalTitle = TextBoxTitle.Text;
                }
                else
                {
                    ((MainWindow)Application.Current.MainWindow).cm.TrackSetTitle(originalTitle, TextBoxTitle.Text);
                    ((MainWindow)Application.Current.MainWindow).cm.TrackSetAlbum(originalTitle, TextBoxAlbum.Text);
                }
                var artistList = new List<string>();
                artistList.AddRange(TextBlockArtists.Text.Split("; "));
                if (((MainWindow)Application.Current.MainWindow).cm.RetrieveTrackArtistNames(originalTitle) != artistList)
                {
                    var originalArtists = ((MainWindow)Application.Current.MainWindow).cm.RetrieveTrackArtistNames(originalTitle);
                    foreach (var artist in originalArtists)
                    {
                        ((MainWindow)Application.Current.MainWindow).cm.UnjoinTrackFromArtist(originalTitle, artist);
                    }
                    foreach (var artist in artistList)
                    {
                        ((MainWindow)Application.Current.MainWindow).cm.AddNewArtist(artist);
                        ((MainWindow)Application.Current.MainWindow).cm.JoinTrackToArtist(originalTitle, artist);
                    }
                }
                ((MainWindow)Application.Current.MainWindow).cm.DeleteUnusedArtists();
                if (TextBoxDisc.Text != "") ((MainWindow)Application.Current.MainWindow).cm.TrackSetDiscNumber(originalTitle, TextBoxDisc.Text);
                if (TextBoxTotalDiscs.Text != "") ((MainWindow)Application.Current.MainWindow).cm.AlbumSetTotalDiscs(TextBoxAlbum.Text, TextBoxTotalDiscs.Text);
                if (TextBoxTrack.Text != "") ((MainWindow)Application.Current.MainWindow).cm.TrackSetTrackNumber(originalTitle, TextBoxTrack.Text);
                if (TextBoxTotalTracks.Text != "") ((MainWindow)Application.Current.MainWindow).cm.AlbumSetTotalTracks(TextBoxAlbum.Text, TextBoxTotalTracks.Text);
                if (TextBoxGenre.Text != "") ((MainWindow)Application.Current.MainWindow).cm.TrackSetGenre(originalTitle, TextBoxGenre.Text);
                if (TextBoxYear.Text != "") ((MainWindow)Application.Current.MainWindow).cm.AlbumSetYear(TextBoxAlbum.Text, TextBoxYear.Text);
                ((MainWindow)Application.Current.MainWindow).ShowAllTracks();
                ((MainWindow)Application.Current.MainWindow).ShowAllAlbums();
                ((MainWindow)Application.Current.MainWindow).ShowAllArtists();
                ((MainWindow)Application.Current.MainWindow).SelectTrack(TextBoxTitle.Text);
                Close();
            }
        }

        
    }
}
