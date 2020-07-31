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
    /// Interaction logic for AlbumDialog.xaml
    /// </summary>
    public partial class AlbumDialog : Window
    {
        private bool isNew = true;
        private string originalTitle;
        public AlbumDialog()
        {
            InitializeComponent();
        }


        public AlbumDialog(string title, string year = "", string discs = "", string tracks = "")
        {
            InitializeComponent();
            TextBoxTitle.Text = title;
            originalTitle = title;
            TextBoxYear.Text = year;
            TextBoxDiscs.Text = discs;
            TextBoxTracks.Text = tracks;
            isNew = false;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxTitle.Text != "")
            {
                if (isNew)
                {
                    ((MainWindow)Application.Current.MainWindow).cm.AddNewAlbum(TextBoxTitle.Text);
                    originalTitle = TextBoxTitle.Text;
                }
                else
                {
                    ((MainWindow)Application.Current.MainWindow).cm.AlbumSetTitle(originalTitle, TextBoxTitle.Text);
                }
                if (TextBoxYear.Text != "") ((MainWindow)Application.Current.MainWindow).cm.AlbumSetYear(originalTitle, TextBoxYear.Text);
                if (TextBoxDiscs.Text != "") ((MainWindow)Application.Current.MainWindow).cm.AlbumSetTotalDiscs(originalTitle, TextBoxDiscs.Text);
                if (TextBoxYear.Text != "") ((MainWindow)Application.Current.MainWindow).cm.AlbumSetTotalTracks(originalTitle, TextBoxTracks.Text);

                ((MainWindow)Application.Current.MainWindow).ShowAllAlbums();
                ((MainWindow)Application.Current.MainWindow).ShowAllTracks();
                ((MainWindow)Application.Current.MainWindow).SelectAlbum(TextBoxTitle.Text);
                Close();
            }
        }
    }
}
