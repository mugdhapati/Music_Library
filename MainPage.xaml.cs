using GEMM_Music.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GEMM_Music
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Music> Musics;
        private List<MenuItem> MenuItems;

        public MainPage()
        {
            this.InitializeComponent();
            Musics = new ObservableCollection<Music>();
            MusicManager.GetAllMusic(Musics);

            MenuItems = new List<MenuItem>();
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/bruno.png", Category = MusicCategory.Brunos });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/demi.png", Category = MusicCategory.Demis });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/drake.png", Category = MusicCategory.Drakes });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/selena.png", Category = MusicCategory.Selenas });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/MyPlaylist.png", Category = MusicCategory.MyPlaylist });

            BackButton.Visibility = Visibility.Collapsed;
            ArtistTextBlock.Visibility = Visibility.Collapsed;
            AlbumTextBlock.Visibility = Visibility.Collapsed;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MysplitView.IsPaneOpen = !MysplitView.IsPaneOpen;
        }

        private void MenuItemsListview_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;
            CategoryTextBlock.Text = menuItem.Category.ToString();
            MusicManager.GetMusicsByCategory(Musics, menuItem.Category);
            BackButton.Visibility = Visibility.Visible;
            SoundGridView.Visibility = Visibility.Visible;
            mySearchBox.Visibility = Visibility.Visible;
            ArtistTextBlock.Visibility = Visibility.Visible;
            AlbumTextBlock.Visibility = Visibility.Visible;
            //MusicTitle.Visibility = Visibility.Visible;
            MusicDetails.Visibility = Visibility.Collapsed;
            MyMediaElement.Visibility = Visibility.Collapsed;
        }

        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var music = (Music)e.ClickedItem;
            //MyMediaElement.Source = new Uri(BaseUri, music.AudioFile);
            MyMediaElement.Source = MediaSource.CreateFromUri(new Uri(BaseUri, music.AudioFile));
            SelectedImage.ImageSource = new BitmapImage(new Uri(BaseUri, music.ImageFile));
            //ArtistTextBlock.Text = music.Artist;
            //AlbumTextBlock.Text = music.Album;
            //M//
            ArtistTextBlock.Text = music.Artist;
            AlbumTextBlock.Text = music.Album;
            ArtistTextBlock.Visibility = Visibility.Visible;
            AlbumTextBlock.Visibility = Visibility.Visible;
            //ArtistTextBlock.Visibility = Visibility.Collapsed;
            SoundGridView.Visibility = Visibility.Collapsed;
            mySearchBox.Visibility = Visibility.Collapsed;
            CategoryTextBlock.Visibility = Visibility.Collapsed;
            MyMediaElement.Visibility = Visibility.Visible;
            MusicDetails.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

            try
            {

                var file = TagLib.File.Create(music.AudioFile);
                MusicTitle.Text = file.Tag.Title;
               // AlbumName.Text = file.Tag.Album;
                
            }
            catch
            {
                Console.WriteLine("exception handled");
                MusicTitle.Text = music.Name;
              //  AlbumName.Text = music.Album;
            }

        }
   
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MusicManager.GetAllMusic(Musics);
            CategoryTextBlock.Text = "All Musics";
            MenuItemsListview.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
            SoundGridView.Visibility = Visibility.Visible;
            mySearchBox.Visibility = Visibility.Visible;
            ArtistTextBlock.Visibility = Visibility.Collapsed;
            AlbumTextBlock.Visibility = Visibility.Collapsed;
        }

        private void mySearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            MusicManager.SearchByName(Musics, args.QueryText);
        }
    }
}
