using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bird_CourseProject_Part1
{
    public class Song
    {
        private string title;
        private string artist;
        private string genre;
        private int year;
        private string url;

        public Song()
        {
            title = "N/A";
            artist = "N/A";
            genre = "N/A";
            year = 0;
            url = "N/A";
        }

        public Song(string title, string artist, string genre, int year, string url)
        {
            this.title = title;
            this.artist = artist;
            this.genre = genre;
            this.year = year;
            this.url = url;
        }

        public override string ToString()
        {
            return title;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public string URL
        {
            get { return url; }
            set { url = value; }
        }
    }
}
