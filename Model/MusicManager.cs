using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEMM_Music.Model
{
    public static class MusicManager
    {
        //private static string MY_SONGS_PATH = "Assets/Music/MyPlaylist";
        private static string MY_SONGS_PATH = "C:/Users/Kaure/source/KalAcademy/GEMM-Music/GEMM Music/GEMM Music/GEMM Music/bin/x86/Debug/AppX/MyPlaylist";
        //private static string MY_SONGS_PATH = @"C:\Users\user1\source\repos\WIP\GEMM Music\GEMM Music\bin\x86\Debug\AppX\MyPlaylist";


       // C:\Users\Kaure\source\KalAcademy\GEMM-Music\GEMM Music\GEMM Music\GEMM Music\bin\x86\Debug\AppX\MyPlaylist

        public static void GetAllMusic(ObservableCollection<Music> musics)
        {
            var allMusics = getMusics();
            musics.Clear();
            allMusics.ForEach(music => musics.Add(music));
        }

        public static void GetMusicsByCategory(ObservableCollection<Music> musics, MusicCategory category)
        {
            var allMusics = getMusics();
            var filteredMusics = allMusics.Where(music => music.Category == category).ToList();
            musics.Clear();
            filteredMusics.ForEach(music => musics.Add(music));
        }

        private static List<Music> getMusics()
        {
            var musics = new List<Music>();

            //M//
            //musics.Add(new Music("Magic", MusicCategory.Brunos));
            //musics.Add(new Music("Uptown", MusicCategory.Brunos));
            //musics.Add(new Music("Confident", MusicCategory.Demis));
            //musics.Add(new Music("Sorry", MusicCategory.Demis));
            //musics.Add(new Music("Hotline", MusicCategory.Drakes));
            //musics.Add(new Music("Scorpion", MusicCategory.Drakes));
            //musics.Add(new Music("LoseU", MusicCategory.Selenas));
            //musics.Add(new Music("StarsDance", MusicCategory.Selenas));
            ////musics.Add(new Music("CheapThrills", MusicCategory.Demis));

            musics.Add(new Music("Magic", MusicCategory.Brunos, "24K Magic", "Bruno Mars"));
            musics.Add(new Music("Uptown", MusicCategory.Brunos, "UpTownFunk", "Mark Ronson"));
            musics.Add(new Music("Confident", MusicCategory.Demis, "ConfidentR", "Demi Lovato"));
            musics.Add(new Music("Sorry", MusicCategory.Demis, "Souveniers", "Demis Rousso"));
            musics.Add(new Music("Hotline", MusicCategory.Drakes, "Hotline Bling", "Drake Graham"));
            musics.Add(new Music("Scorpion", MusicCategory.Drakes, "Scorpion 2018", "Drake Graham"));
            musics.Add(new Music("LoseU", MusicCategory.Selenas, "Lose You to Love Me", "Selena Gomez"));
            musics.Add(new Music("StarsDance", MusicCategory.Selenas, "Stars Dance 2013", "Selena Gomez"));

            var mySongs = getMySongs();
            musics.AddRange(mySongs);

            return musics;
        }

        private static List<Music> getMySongs()
        {
            var mySongs = new List<Music>();
            string[] fileEntries = Directory.GetFiles(MY_SONGS_PATH);
            foreach (string audioFilePath in fileEntries)
            {
                var file = TagLib.File.Create(audioFilePath);
                // mySongs.Add(new Music(file.Tag.Title, MusicCategory.MyPlaylist, audioFilePath, file.Tag.Album));
                //Music music = new Music(file.Tag.Title, MusicCategory.MyPlaylist, audioFilePath, file.Tag.Album);
                string artistname = string.Empty;
                if (file.Tag.Artists != null && file.Tag.Artists.Length > 0)
                    artistname = file.Tag.Artists[0];
                Music music = new Music(file.Tag.Album, file.Tag.Title, MusicCategory.MyPlaylist, audioFilePath, artistname);
                //  Music music = new Music(file.Tag.Title, MusicCategory.MyPlaylist, audioFilePath, file.Tag.Album, file.Tag.Artist);
                if (music.Name == null)
                {
                    music.Name = " ";
                }
                mySongs.Add(music);
            }
            return mySongs;
        }

        /* Search button - Searching based on name and category - starts here */
        public static void SearchByName(ObservableCollection<Music> musics, string queryText)
        {
            var allsongs = getMusics();
            var SearchedSongsByNameCategory =
                allsongs.Where(music => music.Name.ToUpper().Contains(queryText.ToUpper())
                || music.Artist != null && music.Artist.ToString().ToUpper().Contains(queryText.ToUpper())
                || music.Category.ToString().ToUpper().Contains(queryText.ToUpper())).ToList();
            musics.Clear();
            SearchedSongsByNameCategory.ForEach(music => musics.Add(music));
            /* Search button - Searching based on name and category - ends here */
        }


    }
}
