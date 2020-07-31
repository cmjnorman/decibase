using System.Windows;

namespace Decibase_View
{
    /// <summary>
    /// Interaction logic for NewArtistDialog.xaml
    /// </summary>
    public partial class ArtistDialog : Window
    {
        private bool isNew = true;
        private string originalName;
        public ArtistDialog()
        {
            InitializeComponent();
        }

        public ArtistDialog(string name)
        {
            InitializeComponent();
            TextBoxName.Text = name;
            originalName = name;
            isNew = false;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxName.Text != "")
            {
                if (isNew)
                {
                    ((SelectArtistDialog)this.Owner).AddArtist(TextBoxName.Text);
                }
                else 
                {
                    ((MainWindow)Application.Current.MainWindow).cm.ArtistSetName(originalName, TextBoxName.Text);
                    ((MainWindow)Application.Current.MainWindow).ShowAllTracks();
                    ((MainWindow)Application.Current.MainWindow).ShowAllAlbums();
                    ((MainWindow)Application.Current.MainWindow).ShowAllArtists();
                    ((MainWindow)Application.Current.MainWindow).SelectArtist(TextBoxName.Text);
                }
                Close();
            }
        }
    }
}
