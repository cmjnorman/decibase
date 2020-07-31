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
    /// Interaction logic for SelectArtistDialog.xaml
    /// </summary>
    public partial class SelectArtistDialog : Window
    {
        public SelectArtistDialog()
        {
            InitializeComponent();
            var artistList = new List<string>() { "New Artist..." };
            artistList.AddRange(((MainWindow)Application.Current.MainWindow).cm.RetrieveAllArtistNames());
            ComboBoxArtist.ItemsSource = artistList;
        }

        private void ComboBoxArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxArtist.SelectedItem != null)
            {
                if (ComboBoxArtist.SelectedItem.ToString() == "New Artist...")
                {
                    var ad = new ArtistDialog();
                    ad.Owner = this;
                    ad.ShowDialog();
                    Close();
                }
                else
                {
                    AddArtist(ComboBoxArtist.SelectedItem.ToString());
                    Close();
                }
            }  
        }
        public void AddArtist(string artistName)
        {
            ((TrackDialog)this.Owner).AddArtist(artistName);
        }
    }
}
