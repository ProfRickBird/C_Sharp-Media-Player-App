using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Bird_CourseProject_Part1
{
    public partial class MainForm : Form
    {
        // class level references
        String filename = "Songs.txt";

        // constructor
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // read from file
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename);

                // read the song
                while (sr.Peek() > -1)
                {
                    // read the song
                    Song song = new Song();
                    song.Title = sr.ReadLine();
                    song.Artist = sr.ReadLine();
                    song.Genre = sr.ReadLine();
                    song.Year = int.Parse(sr.ReadLine());
                    song.URL = sr.ReadLine();

                    // show the title in the list
                    songList.Items.Add( song );
                }

                // close the file
                sr.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // open the file
            StreamWriter sw = new StreamWriter(filename, false);

            // write the data
            foreach (Song song in songList.Items)
            {
                sw.WriteLine(song.Title);
                sw.WriteLine(song.Artist);
                sw.WriteLine(song.Genre);
                sw.WriteLine(song.Year);
                sw.WriteLine(song.URL);
            }

            // close the file
            sw.Close();
        }

        private bool ValidInput()
        {
            // return true if everything is good
            bool isValid = true;

            if (string.IsNullOrEmpty(titleText.Text))
            {
                MessageBox.Show("The song title cannot be blank.");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(artistText.Text))
            {
                MessageBox.Show("The artist cannot be blank.");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(genreComboBox.Text))
            {
                MessageBox.Show("The genre cannot be blank.");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(yearText.Text))
            {
                MessageBox.Show("The year cannot be blank.");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(urlText.Text))
            {
                MessageBox.Show("The url cannot be blank.");
                isValid = false;
            }
            
            return isValid;
        }

        private void addSongButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(outputText.Text);
            string nl = "\r\n";

            if( ValidInput() )
            {
                // add song information to a new Song object
                Song song = new Song();
                song.Title = titleText.Text;
                song.Artist = artistText.Text;
                song.Genre = genreComboBox.Text;
                song.Year = int.Parse(yearText.Text);
                song.URL = urlText.Text;

                // add to the listbox
                songList.Items.Add(song);

                // all is good
                sb.Append(titleText.Text);
                sb.Append(nl);
                sb.Append(artistText.Text);
                sb.Append(nl);
                sb.Append(genreComboBox.Text);
                sb.Append(nl);
                sb.Append(yearText.Text);
                sb.Append(nl);
                sb.Append(urlText.Text);
                sb.Append(nl);
                sb.Append(nl);

                outputText.Text = sb.ToString();

                // clear out input fields
                titleText.Clear();
                artistText.Clear();
                genreComboBox.Text = "";
                yearText.Clear();
                urlText.Clear();
                titleText.Focus();  // move to the titleText box
            }
        }

        private void songList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Song song = songList.SelectedItem as Song;

            StringBuilder sb = new StringBuilder(string.Empty);
            string nl = "\r\n";

            if( song != null )
            {
                sb.Append(song.Title);
                sb.Append(nl);
                sb.Append(song.Artist);
                sb.Append(nl);
                sb.Append(song.Genre);
                sb.Append(nl);
                sb.Append(song.Year);
                sb.Append(nl);
                sb.Append(song.URL);
                sb.Append(nl);

                outputText.Text = sb.ToString();
            }
        }

        private Song SongInList( string songTitle )
        {
            for( int i = 0; i < songList.Items.Count; ++i )
            {
                Song currentSong = (Song)songList.Items[i];
                if (currentSong.Title.ToLower() == songTitle.ToLower())
                {
                    songList.SelectedIndex = i;
                    return currentSong;
                }
            }

            return null;
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            Song song = SongInList(titleText.Text);

            if ( song != null)
            {
                StringBuilder sb = new StringBuilder(string.Empty);
                string nl = "\r\n";

                sb.Append(song.Title);
                sb.Append(nl);
                sb.Append(song.Artist);
                sb.Append(nl);
                sb.Append(song.Genre);
                sb.Append(nl);
                sb.Append(song.Year);
                sb.Append(nl);
                sb.Append(song.URL);
                sb.Append(nl);

                outputText.Text= sb.ToString();
            }
            else
            {
                MessageBox.Show("Song not found.");
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            titleText.Text = "";
            artistText.Clear();
            genreComboBox.Text = "";
            yearText.Clear();
            urlText.Clear();
            titleText.Focus();   // put insertion point in this field
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Song song = songList.SelectedItem as Song;
            string url = song.URL;
            webViewDisplay.CoreWebView2.Navigate(url);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int index = songList.SelectedIndex;

            if( index > 0 )
            {
                songList.Items.RemoveAt(index);
                outputText.Clear();
            }
            else
            {
                MessageBox.Show("Please select song to delete.");
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
